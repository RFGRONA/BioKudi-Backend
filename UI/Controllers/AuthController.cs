using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Infrastructure.Services;
using Biokudi_Backend.UI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Biokudi_Backend.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController
            (IPersonService _personService, CaptchaService _captchaService, AuthService _authService, 
        CookiesService _cookieService, RSAUtility _rsaUtility, IWebHostEnvironment _env)
            : ControllerBase
    {
        private readonly IPersonService _personService = _personService;
        private readonly CaptchaService _captchaService = _captchaService;
        private readonly AuthService _authService = _authService;
        private readonly CookiesService _cookieService = _cookieService;
        private readonly RSAUtility _rsaUtility = _rsaUtility;
        private readonly IWebHostEnvironment _env = _env;

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterPerson([FromBody] RegisterRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(AuthMessages.InvalidModel);
                if (!_env.IsDevelopment() && !await _captchaService.VerifyCaptcha(request.CaptchaToken))
                    return BadRequest(AuthMessages.CaptchaInvalid);
                var result = await _personService.RegisterPerson(request);
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
                    return BadRequest(AuthMessages.InvalidModel);
                if (!_env.IsDevelopment() && !await _captchaService.VerifyCaptcha(request.CaptchaToken))
                    return BadRequest(AuthMessages.CaptchaInvalid);
                var result = await _personService.LoginPerson(request);
                var token = _authService.GenerateTokens(result.UserId.ToString(), result.Role);
                _cookieService.SetAuthCookies(HttpContext, token.JwtToken, token.RefreshToken, request.RememberMe);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("check-session")]
        [Authorize]
        public async Task<IActionResult> CheckSession()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
                    return BadRequest(AuthMessages.InvalidSession);

                var result = await _personService.GetPersonById(parsedUserId);
                if (result == null) return NotFound(AuthMessages.PersonNotFound);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            _cookieService.RemoveCookies(HttpContext);
            return Ok(AuthMessages.Logout);
        }

        [HttpGet("public-key")]
        public ActionResult GetPublicKey()
        {
            try
            {
                string publicKey = _rsaUtility.GetPublicKey();
                return Content(publicKey, "text/plain");
            }
            catch (Exception ex)
            {
                return StatusCode(500, AuthMessages.PublicKeyError);
            }
        }

        [HttpPost("encrypt-password")]
        public ActionResult EncryptPassword([FromBody] PasswordRequest request)
        {
            if (!_env.IsDevelopment()) return NotFound();
            try
            {
                string encryptedPassword = _rsaUtility.EncryptWithPublicKey(request.Password);
                return Ok(encryptedPassword);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
