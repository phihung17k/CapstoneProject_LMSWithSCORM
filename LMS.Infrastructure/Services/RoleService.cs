using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels.RoleRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Utils;

namespace LMS.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionRoleRepository _permissionRoleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RoleService(IRoleRepository roleRepository, IPermissionRepository permissionRepository,
        IPermissionRoleRepository permissionRoleRepository, IMapper mapper, IUnitOfWork unitOfWork,
        IRefreshTokenRepository refreshTokenRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _permissionRoleRepository = permissionRoleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public Task<PagingViewModel<RoleViewModelWithoutPermission>> Search(RolePagingRequestModel rolePagingRequestModel)
        {
            if (rolePagingRequestModel.Name == null)
            {
                rolePagingRequestModel.Name = "";
            }
            var resultByCondition = _roleRepository.Get(r => r.Name.ToLower().Contains(rolePagingRequestModel.Name.ToLower()));

            if (!resultByCondition.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            IOrderedQueryable<Role> orderedResult = null;
            if (rolePagingRequestModel.NameSort == null)
            {
                orderedResult = resultByCondition.OrderByDescending(r => r.CreateTime);
            }
            if (rolePagingRequestModel.NameSort == SortOrder.Descending)
            {
                orderedResult = resultByCondition.OrderByDescending(r => r.Name);
            }
            if (rolePagingRequestModel.NameSort == SortOrder.Ascending)
            {
                orderedResult = resultByCondition.OrderBy(r => r.Name);
            }
            var result = orderedResult
                                        .Skip((rolePagingRequestModel.CurrentPage - 1) * rolePagingRequestModel.PageSize)
                                        .Take(rolePagingRequestModel.PageSize);
            if (!result.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            List<RoleViewModelWithoutPermission> items = _mapper.Map<List<RoleViewModelWithoutPermission>>(result.ToList());
            foreach (var role in items)
            {
                if (role.Id == 1) //authen user
                {
                    role.CanDelete = false;
                    role.CanDeactive = false;
                    role.CanAssign = false;
                } else if (role.Id == 2) //admin
                {
                    role.CanDelete = false;
                    role.CanDeactive = false;
                } else if (role.Id == 3)
                {
                    role.CanDelete = false;
                } //student
            }
            return Task.FromResult(new PagingViewModel<RoleViewModelWithoutPermission>
                                            (items, resultByCondition.Count(),
                                            rolePagingRequestModel.CurrentPage,
                                            rolePagingRequestModel.PageSize));
        }

        public async Task<RoleViewModel> CreateRole(RoleCreateRequestModel roleRequestModel)
        {
            // Validate request data
            ValidateUtils.CheckStringNotEmpty("role.Name", roleRequestModel.Name);

            //Validate role name exist or not
            var checkRoleNameExist = _roleRepository.Get(r => r.Name.ToLower() == roleRequestModel.Name.ToLower());
            if (checkRoleNameExist.FirstOrDefault() != null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.RoleNameExist, ErrorMessages.RoleNameExist);
            }

            // Add role
            var role = await AddRoleToDatabase(roleRequestModel);
            return _mapper.Map<RoleViewModel>(role);
        }

        private async Task<Role> AddRoleToDatabase(RoleCreateRequestModel roleRequestModel)
        {
            var role = _mapper.Map<Role>(roleRequestModel);
            role.Permissions = null;
            await _roleRepository.AddAsync(role);
            await _unitOfWork.SaveChangeAsync();

            if (roleRequestModel.Permissions != null && roleRequestModel.Permissions.Any())
            {
                foreach (var permission in roleRequestModel.Permissions)
                {
                    var addedPermission = await _permissionRepository.FindAsync((int)permission.PermissionId);
                    ValidateUtils.CheckDataNotNull("permission", addedPermission);

                    var permissionRole = new PermissionRole()
                    {
                        Role = role,
                        Permission = addedPermission,
                    };
                    await _permissionRoleRepository.AddAsync(permissionRole);
                    await _unitOfWork.SaveChangeAsync();
                }
            }

            return role;
        }

        public Task<RoleViewModel> Get(int roleId)
        {
            var role = _roleRepository.Get(r => r.Id == roleId, r => r.Permissions).FirstOrDefault();
            if (role == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            return Task.FromResult(_mapper.Map<RoleViewModel>(role));
        }

        public async Task<RoleViewModel> Update(int roleId, RoleUpdateRequestModel roleRequestModel)
        {
            // Validate request data
            ValidateUtils.CheckStringNotEmpty("role.Name", roleRequestModel.Name);

            //Validate role name exist or not
            var checkRoleNameExist = _roleRepository.Get(r => r.Name.ToLower() == roleRequestModel.Name.ToLower() && r.Id != roleId);
            if (checkRoleNameExist.FirstOrDefault() != null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.RoleNameExist, ErrorMessages.RoleNameExist);
            }

            var role = _mapper.Map<Role>(roleRequestModel);
            role.Id = roleId;
            var permissionInRoleInDB = _permissionRoleRepository
                                            .Get(r => r.RoleId == role.Id)
                                            .Select(r => r)
                                            .AsEnumerable();
            var permissionRoleInRequest = role.Permissions.AsEnumerable();
            permissionRoleInRequest = permissionRoleInRequest.Select(pr => { pr.RoleId = roleId; return pr; });
            var permissionInDBNotRequest = permissionInRoleInDB.Where(x => !permissionRoleInRequest.Any(y => y.PermissionId == x.PermissionId));
            var permissionInRequestNotDB = permissionRoleInRequest.Where(x => !permissionInRoleInDB.Any(y => y.PermissionId == x.PermissionId));

            if (!permissionInDBNotRequest.Any() && !permissionInRequestNotDB.Any())
            {
                _roleRepository.Update(role);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<RoleViewModel>(role);
            }

            role.Permissions = null;
            _permissionRoleRepository.RemoveRange(permissionInDBNotRequest);
            await _permissionRoleRepository.AddRange(permissionInRequestNotDB);
            _roleRepository.Update(role);
            await _unitOfWork.SaveChangeAsync();

            _refreshTokenRepository.RevokeAllTokenByRoleId(role.Id);

            return _mapper.Map<RoleViewModel>(role);
        }

        public async Task Delete(int roleId)
        {
            //cannot delete roleId = 1 (Authen User), 2 (Admin), 3(Student)
            if (roleId == 1 || roleId == 2 || roleId == 3)
            {
                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.CannotPerformAction, ErrorMessages.CannotPerformAction);
            }
            var role = _roleRepository.FindAsync(roleId).Result;
            if (role == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            _refreshTokenRepository.RevokeAllTokenByRoleId(roleId);

            await _roleRepository.Remove(roleId);

            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<RoleViewModelWithoutPermission> UpdateStatus(int roleId, bool isActive)
        {
            //cannot deactive roleId = 1 (Authen User), 2 (Admin)
            if ((roleId == 1 || roleId == 2) && !isActive)
            {
                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.CannotPerformAction, ErrorMessages.CannotPerformAction);
            }
            var role = _roleRepository.Get(r => r.Id == roleId).FirstOrDefault();
            if (role == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            role.IsActive = isActive;

            _refreshTokenRepository.RevokeAllTokenByRoleId(roleId);

            _roleRepository.Update(role);

            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<RoleViewModelWithoutPermission>(role);
        }
    }
}