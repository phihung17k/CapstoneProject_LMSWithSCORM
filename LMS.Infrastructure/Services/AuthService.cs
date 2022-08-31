using AutoMapper;
using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IService;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenService jwt;
        private readonly IUserRepository userRepo;
        private readonly IRefreshTokenRepository refreshTokenRepo;
        private readonly IHttpClientFactory clientFactory;

        public AuthService(IJwtTokenService jwt,
            IUserRepository userRepo, IRefreshTokenRepository refreshTokenRepo,
            IHttpClientFactory clientFactory)
        {
            this.jwt = jwt;
            this.userRepo = userRepo;
            this.refreshTokenRepo = refreshTokenRepo;
            this.clientFactory = clientFactory;
        }

        public async Task<LoginResponseModel> Authenticate(LoginRequestModel requestModel)
        {
            UserModel userModel;
            HttpClient client = clientFactory.CreateClient(StringUtils.ClientString);
            HttpResponseMessage response = await client.PostAsJsonAsync("api/users/login", requestModel);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(content);
            }
            userModel = await response.Content.ReadAsAsync<UserModel>();
            //if (userModel is null)
            //{
            //    throw new Exception();
            //}
            //else if (!IsAccessibleLMS(userModel))
            //{
            //    throw new AccessibleException("The account is not allowed to access the system");
            //}

            User searchedUser = await userRepo.FindAsync(userModel.UserId);
            List<Permission> permissions = new();
            if (searchedUser is null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            else
            {
                if (!searchedUser.IsActiveInLMS || !searchedUser.IsActive || searchedUser.IsDeleted)
                {
                    throw new AccessibleException("The account is not allowed to access the system");
                }
                else
                {
                    permissions = userRepo.GetPermissionsById(searchedUser.Id);
                }
            }

            LoginResponseModel responseModel = await GetResponse(searchedUser.Id, permissions);

            return responseModel;
        }

        private bool IsAccessibleLMS(UserModel userModel)
        {
            foreach (var systemModule in userModel.SystemModules)
            {
                if (systemModule.Name.Equals("LMS") && systemModule.IsActive)
                {
                    return true;
                }
            }
            return false;
        }

        private async Task<LoginResponseModel> GetResponse(Guid userId,
            List<Permission> permissions, bool isRefresh = false)
        {
            User user = await userRepo.FindAsync(userId);

            #region revoke all the existing access token and refresh token in db

            //use for login multiple ways, tabs
            if (!isRefresh)
            {
                refreshTokenRepo.RevokeAllTokenByUserId(user.Id.ToString());
            }

            #endregion

            #region save refresh token to db

            AccessTokenInfomationModel accessTokenInfo = jwt.CreateToken(user, permissions);
            RefreshToken refreshToken = GenerateRefreshToken(user, accessTokenInfo.AccessToken);
            await refreshTokenRepo.AddAsync(refreshToken);

            #endregion
            List<string> permissionsString = permissions.Select(p => $"{p.Category}.{p.Code}").ToList();

            return new LoginResponseModel(user, accessTokenInfo, refreshToken.Content, permissionsString);
        }

        private RefreshToken GenerateRefreshToken(User user, string accessToken)
        {
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Content = RandomString(35) + Guid.NewGuid(),
                User = user,
                AccessToken = accessToken,
                CreateTime = DateTimeOffset.Now,
                ExpiresTime = DateTimeOffset.Now.AddDays(7)
            };
            return refreshToken;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)]).ToArray());
        }

        public async Task<LoginResponseModel> RefreshToken(TokenRequestModel requestModel)
        {
            string accessToken = requestModel.Token;
            string refreshTokenString = requestModel.RefreshToken;

            RefreshToken refreshToken = refreshTokenRepo.FindByToken(refreshTokenString);

            if (refreshToken is null)
            {
                throw new HttpRequestException("refresh token is not match");
            }

            if (!refreshToken.AccessToken.Equals(accessToken))
            {
                throw new HttpRequestException("Access token is not valid");
            }

            User user = refreshToken.User;

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            //Validation 1: check well-formed JWT token
            if (!handler.CanReadToken(accessToken))
            {
                throw new HttpRequestException("Access token is not valid");
            }
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(accessToken);

            //Validation 2: check encryption alogrithm
            string signatureAlgorithm = jwtSecurityToken.SignatureAlgorithm;
            bool isMatchAlgorithm = signatureAlgorithm.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
            if (isMatchAlgorithm == false)
            {
                throw new HttpRequestException("Access token is not valid");
            }

            // Validation 3: validate expiry date
            List<Claim> claims = jwtSecurityToken.Claims.ToList();
            var utcExpiryDate = long.Parse(claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            DateTime expiryTime = DatetimeUtils.UnixTimeStampToDateTime(utcExpiryDate);
            if (expiryTime >= DateTime.Now)
            {
                throw new HttpRequestException("Access token has not expired yet");
            }

            //Validation 4: userId in access token is match userId in db (refreshToken entity)
            string userId = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (!userId.Equals(refreshToken.User.Id.ToString()))
            {
                throw new HttpRequestException("User is not match");
            }

            //Validation 5: check refresh token is revoked
            if (refreshToken.RevokedTime is not null && refreshToken.RevokedTime <= DateTimeOffset.Now)
            {
                throw new HttpRequestException("Refresh token is used");
            }

            refreshToken.RevokedTime = DateTimeOffset.Now;
            refreshTokenRepo.Update(refreshToken);

            List<Permission> permissions = userRepo.GetPermissionsById(user.Id);

            return await GetResponse(user.Id, permissions, isRefresh: true);
        }

        public async Task Logout(string userId)
        {
            try
            {
                Guid id = Guid.Parse(userId);
                User user = await userRepo.FindAsync(id);
                if (user is null)
                {
                    throw new HttpRequestException("User id is null");
                }
                refreshTokenRepo.RevokeAllTokenByUserId(user.Id.ToString());
            }
            catch (ArgumentNullException)
            {
                throw new HttpRequestException("Input is empty");
            }
            catch (FormatException)
            {
                throw new HttpRequestException("User id is in the wrong format");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
