using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QwiikTechnicalTest.Interfaces;
using QwiikTechnicalTest.Models;
using QwiikTechnicalTest.Models.DTO.User;
using QwiikTechnicalTest.Utilities;
using QwikkTechnicalTest.Services;
using System.Reflection.Metadata;

namespace QwiikTechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        public UserController(IUser userService)
        {
            _userService = userService;
        }

        [HttpPost("login-admin")]
        public async Task<IActionResult> SignInUser([FromBody] UserLoginDTO dto)
        {
            try
            {
                var isSuccess = await _userService.SignInUser(dto);
                if (!isSuccess)
                {
                    return Ok(GeneralResponse<string>.Fail(Constants.LoginFailed));
                }

                return Ok(GeneralResponse<string>.Success(Constants.LoginSuccess));
            }
            catch (Exception)
            {
                return BadRequest(GeneralResponse<string>.Fail(Constants.GeneralErrorMessage));
                throw;
            }
        }
    }
}
