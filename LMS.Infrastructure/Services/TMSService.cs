using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services
{
    public class TMSService : ITMSService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly TMSRepository _tmsRepository;

        public TMSService(IConfiguration configuration, IHttpClientFactory clientFactory, TMSRepository tmsRepository)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _tmsRepository = tmsRepository;
        }

        public async Task Authenticate()
        {
            string username = _configuration["TMSLogin:Username"];
            string password = _configuration["TMSLogin:Password"];

            UserModel userModel;
            HttpClient client = _clientFactory.CreateClient(StringUtils.ClientString);
            HttpResponseMessage response = await client.PostAsJsonAsync("api/users/login", new
            {
                Username = username,
                Password = password
            });
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(content);
            }
            userModel = await response.Content.ReadAsAsync<UserModel>();
            if (userModel is null)
            {
                throw new Exception();
            }
            else if (!IsAccessibleLMS(userModel))
            {
                throw new AccessibleException("The account is not allowed to access the system");
            }

            //read access token
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(userModel.TMSAccessToken);
            List<Claim> claims = jwtSecurityToken.Claims.ToList();
            var utcExpiryDate = long.Parse(claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            DateTime expiryTime = DatetimeUtils.UnixTimeStampToDateTime(utcExpiryDate);

            //save access token and expire
            _tmsRepository.AccessToken = userModel.TMSAccessToken;
            _tmsRepository.ExpirationDate = expiryTime;
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

        public async Task VerifyAuthentication()
        {
            string accessToken = _tmsRepository.AccessToken;
            DateTime expireTime = _tmsRepository.ExpirationDate;
            if (accessToken is null || expireTime < DateTime.UtcNow)
            {
                await Authenticate();
            }
        }
    }
}
