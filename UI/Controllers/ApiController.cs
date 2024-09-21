using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Infrastructure.Services;
using Biokudi_Backend.UI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController
            (IPersonService _personService, CaptchaService _captchaService, AuthService _authService, 
        CookiesService _cookieService, RSAUtility _rsaUtility)
            : ControllerBase
    {
        private readonly IPersonService _personService = _personService;
        private readonly CaptchaService _captchaService = _captchaService;
        private readonly AuthService _authService = _authService;
        private readonly CookiesService _cookieService = _cookieService;
        private readonly RSAUtility _rsaUtility = _rsaUtility;

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterPerson([FromBody] RegisterRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(PersonMessages.InvalidModel);
                //if (!await _captchaService.VerifyCaptcha(request.CaptchaToken))
                //    return BadRequest(PersonMessages.CaptchaInvalid);
                var result = _personService.RegisterPerson(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginPerson([FromBody] LoginRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(PersonMessages.InvalidModel);
                //if (!await _captchaService.VerifyCaptcha(request.CaptchaToken))
                //    return BadRequest(PersonMessages.CaptchaInvalid);
                var result = await _personService.LoginPerson(request);
                var token = _authService.GenerateJwtToken(result.UserId.ToString(), request.RememberMe);
                _cookieService.SetAuthCookies(HttpContext, token, result.UserId.ToString(), request.RememberMe);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("check-session")]
        [Authorize]
        public async Task<IActionResult> CheckSession()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return Unauthorized(PersonMessages.InvalidSession);
                var userIdCookie = Request.Cookies["userId"];
                if (!int.TryParse(userIdCookie, out int userId))
                    return BadRequest(PersonMessages.InvalidSession);
                var result = await _personService.GetPersonById(userId);
                if (result == null) return NotFound(PersonMessages.PersonNotFound);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _cookieService.RemoveCookies(HttpContext);
            return Ok(PersonMessages.Logout);
        }

        [HttpGet("public-key")]
        public ActionResult<string> GetPublicKey()
        {
            return Ok(_rsaUtility.GetPublicKey());
        }
    }
}
