using AutoMapper;
using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels.UserRequestModel;
using LMS.Core.Models.RequestModels.UserRoleRequestModel;
using LMS.Core.Models.TMSResponseModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IHttpClientFactory factory;
        private readonly IMapper _mapper;
        private readonly IRoleUserRepository _roleUserRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TMSRepository _tmsRepository;
        private readonly IJwtTokenService jwtTokenService;
        private readonly IQuestionRepository questionRepository;
        private readonly ISyncLogRepository syncLogRepository;
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public UserService(IUserRepository repository, IHttpClientFactory factory,
            IMapper mapper, IRoleUserRepository roleUserRepository,
            IUnitOfWork unitOfWork, IRoleRepository roleRepository, TMSRepository tmsRepository, IJwtTokenService jwtTokenService,
            IQuestionRepository questionRepository, ISyncLogRepository syncLogRepository,
            IRefreshTokenRepository refreshTokenRepository)
        {
            this.repository = repository;
            this.factory = factory;
            this._mapper = mapper;
            _roleUserRepository = roleUserRepository;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _tmsRepository = tmsRepository;
            this.jwtTokenService = jwtTokenService;
            this.questionRepository = questionRepository;
            this.syncLogRepository = syncLogRepository;
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task SyncUser()
        {
            HttpClient client = factory.CreateClient(StringUtils.ClientString);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
                _tmsRepository.AccessToken);
            //flag is stop condition to get tms data model, the end point of paging
            bool flag = true;
            //index page in total page
            int index = 1;
            DateTimeOffset startTime = DateTimeOffset.Now;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            do
            {
                string query = $"/api/users/search?index={index}&size=23000&isActive=true&usernameSort=true";
                HttpResponseMessage response = await client.GetAsync(query);
                if (!response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException(content);
                }
                statusCode = response.StatusCode;
                string resultJson = await response.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<UserResponseModel>(resultJson);
                if (userResponse is null)
                {
                    throw new RequestException(HttpStatusCode.InternalServerError, ErrorCodes.ConvertTMSDataModelFail,
                        ErrorMessages.ConvertTMSDataModelFail);
                }
                var userDetails = userResponse.Data;
                var paging = userResponse.Paging;
                if (!paging.HasNext)
                {
                    flag = false;
                }
                else
                {
                    index++;
                }
                //get user isActive = true and isDelete = false
                userDetails = userDetails.Where(ud => ud.IsActive = true && ud.IsDeleted == false).ToList();
                List<User> users = _mapper.Map<List<User>>(userDetails);
                await repository.SynchronizeBulk(users.ToList());
            }
            while (flag);
            DateTimeOffset endTime = DateTimeOffset.Now.AddMinutes(1);

            string jwtToken = _tmsRepository.AccessToken;
            Guid creatorId = jwtTokenService.GetUserIdFromToken(jwtToken);
            List<int> defaultRoleIds = new(new int[] { 1, 2, 3 }); //Authenticated User, Admin, Student
            await repository.AssignRoleForInsertedUser(startTime, endTime, creatorId, defaultRoleIds);

            await syncLogRepository.AddAsync(new SyncLog
            {
                StartTime = startTime,
                EndTime = DateTimeOffset.Now,
                StatusCode = (int)statusCode,
                TableName = "user"
            });
            await _unitOfWork.SaveChangeAsync();
        }


        public Task<UserViewModel> GetDetailUser(Guid userId)
        {
            var userDetail = repository.Get(u => u.Id == userId && 
                                            u.IsActive != false && 
                                            u.IsDeleted != true)
                                       .FirstOrDefault();
            if (userDetail == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            return Task.FromResult(_mapper.Map<UserViewModel>(userDetail));
        }


        public async Task<List<RoleViewModelWithoutPermission>> AssignRoleToUser(string userId, UserRoleRequestModel userRoleRequestModel)
        {
            var userIdGuid = ValidateUtils.CheckGuidFormat(nameof(userId), userId);
            try
            {
                var currentRoles = _roleUserRepository.Get(r => r.UserId.ToString() == userId).AsEnumerable();

                //remove old roles that is not in request except authen user
                _roleUserRepository.RemoveRange(currentRoles.Where(r => !userRoleRequestModel.RoleIds.Contains(r.RoleId) && r.RoleId != 1));

                List<int> listOfUpdatedRoleId = new();
                foreach (var roleId in userRoleRequestModel.RoleIds)
                {
                    var role = await _roleRepository.FindAsync(roleId);
                    if (role != null && role.IsActive != false)
                    {
                        listOfUpdatedRoleId.Add(roleId);
                    }
                }

                IEnumerable<RoleUser> newRoles = listOfUpdatedRoleId.Select(roleId => new RoleUser
                {
                    UserId = userIdGuid,
                    RoleId = roleId
                });

                //add new roles that is not in db
                await _roleUserRepository.AddRange(newRoles.Where(r => !currentRoles.Select(crr => crr.RoleId).Contains(r.RoleId)));
                bool isSuccess = await _unitOfWork.SaveChangeAsync();
                if (isSuccess)
                {
                    refreshTokenRepository.RevokeAllTokenByUserId(userId);
                }
            }
            catch (DbUpdateException)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return await GetRolesByUserId(userId);
        }

        public async Task<UserRoleViewModel> GetDetailUserRole(string userId)
        {
            var userIdGuid = ValidateUtils.CheckGuidFormat(nameof(userId), userId);
            var userDetailModel = await GetDetailUser(userIdGuid);
            var roles = await GetRolesByUserId(userId);
            var userRoleViewModel = _mapper.Map<UserRoleViewModel>(userDetailModel);
            userRoleViewModel.Roles = roles;
            return userRoleViewModel;
        }

        private Task<List<RoleViewModelWithoutPermission>> GetRolesByUserId(string userId)
        {
            //get roles of user except Authen user
            var rolesUser = _roleUserRepository.Get(r => r.UserId == Guid.Parse(userId) && r.RoleId != 1, r => r.Role);
            List<Role> roles = new();
            if (rolesUser.Any())
            {
                foreach (RoleUser roleUser in rolesUser)
                {
                    roles.Add(roleUser.Role);
                }
            }
            return Task.FromResult(_mapper.Map<List<RoleViewModelWithoutPermission>>(roles));
        }

        public Task<PagingViewModel<UserRoleViewModel>> GetAllUser(UserPagingRequestModel requestModel)
        {
            if(requestModel.ExceptRoleId != null && requestModel.RoleId != null
                && requestModel.ExceptRoleId == requestModel.RoleId)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.SearchedRoleIsNotSameExceptRole, 
                    ErrorMessages.SearchedRoleIsNotSameExceptRole);
            }
            //search
            var result = repository.Get(u => u.IsActive != false && u.IsDeleted != true &&
                (requestModel.Search == null || u.UserName.ToLower().Contains(requestModel.Search.ToLower()) || (u.FirstName + " " + u.LastName).ToLower().Contains(requestModel.Search.ToLower())) &&
                (requestModel.IsMale == null || u.IsMale == requestModel.IsMale) &&
                (requestModel.RoleId == null || u.Roles.Any(role => role.RoleId == requestModel.RoleId)) &&
                (requestModel.IsActive == null || u.IsActiveInLMS == requestModel.IsActive) &&
                (requestModel.ExceptRoleId == null || !u.Roles.Where(r => r.RoleId == requestModel.ExceptRoleId).Any()));

            if (!result.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            //sort
            if (requestModel.UserNameSort == SortOrder.Descending)
            {
                result = result.OrderByDescending(u => u.UserName);
            }
            else if (requestModel.UserNameSort == SortOrder.Ascending)
            {
                result = result.OrderBy(u => u.UserName);
            }
            //paging
            int totalRecord = result.Count();
            result = result.Skip((requestModel.CurrentPage - 1) * requestModel.PageSize)
                                        .Take(requestModel.PageSize);
            //include roles of user except authen user
            result = result.Include(u => u.Roles.Where(ur => ur.RoleId != 1)).ThenInclude(r => r.Role);
            //mapping
            var items = _mapper.Map<List<UserRoleViewModel>>(result.ToList());
            if (!items.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            return Task.FromResult(new PagingViewModel<UserRoleViewModel>
                                           (items, totalRecord,
                                           requestModel.CurrentPage,
                                           requestModel.PageSize));
        }

        public async Task<string> UpdateAvatar(Guid userId, IFormFile image)
        {
            try
            {
                User user = await repository.FindAsync(userId);

                ResourceInfoModel resourceInfo = await FileUtils.SaveFileToFolder(image, isImageFile: true);
                user.Avatar = resourceInfo.Url;
                await _unitOfWork.SaveChangeAsync();
                return resourceInfo.Url;
            }
            catch (ArgumentNullException)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            catch (FormatException)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
        }

        public List<QuestionCreatorViewModel> GetQuestionCreators(int questionBankId)
        {
            var creatorIdList = questionRepository.Get(q => q.QuestionBankId == questionBankId)
                .ToList().Where(q => q.IsDeleted != true).Select(q => q.CreateBy);
            if (creatorIdList.Any())
            {
                creatorIdList = creatorIdList.Distinct();
                var result = new List<QuestionCreatorViewModel>();
                creatorIdList.ToList().ForEach(c =>
                {
                    var fullName = repository.Get(u => u.Id == c).Select(u => u.LastName + " " + u.FirstName).First();
                    result.Add(new QuestionCreatorViewModel
                    {
                        UserId = c,
                        FullName = fullName
                    });
                });
                return result;
            }
            return null;
        }

        public async Task<UserViewModel> UpdateStatus(Guid id, bool isActive)
        {
            var user = repository.Get(u => u.Id == id).FirstOrDefault();
            if (user == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            user.IsActiveInLMS = isActive;
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
