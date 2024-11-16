using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.DTOs.Response;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Infrastructure.Services;
using Biokudi_Backend.UI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
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

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="request">Datos del usuario para registrarse.</param>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterPerson([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo no válido.");

            if (!_env.IsDevelopment() && !await _captchaService.VerifyCaptcha(request.CaptchaToken))
                return BadRequest("Captcha inválido.");

            var result = await _personService.RegisterPerson(request);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);

            return NoContent();
        }

        /// <summary>
        /// Inicia sesión en la aplicación.
        /// </summary>
        /// <param name="request">Credenciales del usuario.</param>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginPerson([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo no válido.");

            if (!_env.IsDevelopment() && !await _captchaService.VerifyCaptcha(request.CaptchaToken))
                return BadRequest("Captcha inválido.");

            var result = await _personService.LoginPerson(request);
            if (result.IsFailure)
                return NotFound(result.ErrorMessage);

            var token = _authService.GenerateTokens(result.Value.UserId.ToString(), result.Value.Role);
            _cookieService.SetAuthCookies(HttpContext, token.JwtToken, token.RefreshToken, request.RememberMe);

            return Ok(result.Value);
        }

        /// <summary>
        /// Verifica si la sesión del usuario es válida.
        /// </summary>
        [HttpGet("check-session")]
        [Authorize]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckSession()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
                return BadRequest(MessagesHelper.InvalidSession);

            var result = await _personService.GetPersonById(parsedUserId);

            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound(MessagesHelper.PersonNotFound);

            return Ok(result.Value);
        }

        /// <summary>
        /// Obtiene el perfil del usuario.
        /// </summary>
        [HttpGet("profile")]
        [Authorize]
        [ProducesResponseType(typeof(ProfileResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
                return BadRequest(MessagesHelper.InvalidSession);

            var result = await _personService.GetUserProfile(parsedUserId);

            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound(MessagesHelper.PersonNotFound);

            return Ok(result.Value);
        }

        /// <summary>
        /// Actualiza el perfil del usuario.
        /// </summary>
        /// <param name="person">Nuevos datos del perfil del usuario.</param>
        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] PersonRequestDto person)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
                return BadRequest(MessagesHelper.InvalidSession);

            var result = await _personService.UpdateUserProfile(parsedUserId, person);

            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (!result.Value)
                return NotFound(MessagesHelper.PersonNotFound);

            return Ok();
        }

        /// <summary>
        /// Elimina el perfil del usuario autenticado.
        /// </summary>
        [HttpDelete("delete-profile")]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
                return BadRequest(MessagesHelper.InvalidSession);

            var result = await _personService.DeleteUser(parsedUserId);

            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (!result.Value)
                return NotFound(MessagesHelper.PersonNotFound);

            return Ok();
        }

        /// <summary>
        /// Actualiza la contraseña del usuario.
        /// </summary>
        /// <param name="request">Datos de la nueva contraseña.</param>
        [HttpPost("update-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequestDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
                return BadRequest(MessagesHelper.InvalidSession);

            var result = await _personService.UpdatePassword(parsedUserId, request);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);

            return Ok();
        }

        /// <summary>
        /// Solicita un token para restablecer la contraseña.
        /// </summary>
        /// <param name="request">Correo electrónico del usuario para restablecer la contraseña.</param>
        [HttpPost("request-reset-password")]
        public async Task<IActionResult> RequestResetToken([FromBody] ForgotPasswordRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo no válido.");

            var result = await _personService.GeneratePasswordResetToken(request.Email);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);

            return Ok(MessagesHelper.RequestForgotPassword);
        }

        /// <summary>
        /// Verifica y restablece la contraseña del usuario.
        /// </summary>
        /// <param name="request">Datos necesarios para restablecer la contraseña.</param>
        [HttpPost("verify-reset-password")]
        public async Task<IActionResult> VerifyResetToken([FromBody] ResetPasswordRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo no válido.");

            var result = await _personService.VerifyAndResetPassword(request);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);

            return Ok(MessagesHelper.VerifyForgotPassword);
        }

        /// <summary>
        /// Cierra la sesión del usuario.
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            _cookieService.RemoveCookies(HttpContext);
            return Ok(MessagesHelper.Logout);
        }

        /// <summary>
        /// Obtiene la clave pública para cifrado.
        /// </summary>
        [HttpGet("public-key")]
        [OutputCache(Duration = 300)]
        public ActionResult GetPublicKey()
        {
            try
            {
                string publicKey = _rsaUtility.GetPublicKey();
                return Content(publicKey, "text/plain");
            }
            catch (Exception ex)
            {
                return StatusCode(500, MessagesHelper.PublicKeyError);
            }
        }

        /// <summary>
        /// Cifra una contraseña utilizando la clave pública.
        /// </summary>
        /// <param name="request">Contraseña a cifrar.</param>
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
                return BadRequest(ex);
            }
        }
    }
}