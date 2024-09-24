﻿using Biokudi_Backend.Application.DTOs.Request;
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
                    return BadRequest(ApiMessages.InvalidModel);
                if (!_env.IsDevelopment() && !await _captchaService.VerifyCaptcha(request.CaptchaToken))
                    return BadRequest(ApiMessages.CaptchaInvalid);
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
                    return BadRequest(ApiMessages.InvalidModel);
                if (!_env.IsDevelopment() && !await _captchaService.VerifyCaptcha(request.CaptchaToken))
                    return BadRequest(ApiMessages.CaptchaInvalid);
                var result = await _personService.LoginPerson(request);
                var token = _authService.GenerateJwtToken(result.UserId.ToString(), request.RememberMe);
                _cookieService.SetAuthCookies(HttpContext, token, result.UserId.ToString(), request.RememberMe);
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
                if (!User.Identity.IsAuthenticated)
                    return Unauthorized(ApiMessages.InvalidSession);
                var userIdCookie = Request.Cookies["userId"];
                if (!int.TryParse(userIdCookie, out int userId))
                    return BadRequest(ApiMessages.InvalidSession);
                var result = await _personService.GetPersonById(userId);
                if (result == null) return NotFound(ApiMessages.PersonNotFound);
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
            return Ok(ApiMessages.Logout);
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
                return StatusCode(500, ApiMessages.PublicKeyError);
            }
        }
    }
}
