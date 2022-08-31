using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IService;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service;

        public AuthController(IAuthService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(LoginResponseModel), 200)]
        public IActionResult Login([FromBody] LoginRequestModel loginRequest)
        {
            try
            {
                LoginResponseModel responseModel = service.Authenticate(loginRequest).GetAwaiter().GetResult();

                return Ok(responseModel);
            }
            catch (HttpRequestException e)
            {
                return Unauthorized(e.Message);
            }
            catch (AccessibleException e)
            {
                return StatusCode(403, e.Message);
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        [ProducesResponseType(typeof(LoginResponseModel), 200)]
        public IActionResult RefreshToken([FromBody] TokenRequestModel tokenRequestModel)
        {
            try
            {
                LoginResponseModel response = service.RefreshToken(tokenRequestModel).GetAwaiter().GetResult();

                if (response != null)
                {
                    return Ok(response);
                }
            }
            catch (HttpRequestException e)
            {
                return Unauthorized(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server exception");
            }
            return BadRequest();
        }
    }
}
