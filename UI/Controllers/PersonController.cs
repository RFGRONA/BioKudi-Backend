using Biokudi_Backend.Application.DTOs.Request;
using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Infrastructure.ExternalServices;
using Biokudi_Backend.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly CaptchaService _captchaService;
        public PersonController(IPersonService _personService, CaptchaService _captchaService)
        {
            this._personService = _personService;
            this._captchaService = _captchaService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterPerson([FromBody] RegisterRequestDto request)
        {
            if(ModelState.IsValid)
            {
                if (!await _captchaService.VerifyCaptcha(request.CaptchaToken))
                {
                    return BadRequest(PersonMessages.CaptchaInvalid);
                }
                var result = _personService.RegisterPerson(request);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginPerson([FromBody] LoginRequestDto request)
        {
            if (ModelState.IsValid)
            {
                if (!await _captchaService.VerifyCaptcha(request.CaptchaToken))
                {
                    return BadRequest(PersonMessages.CaptchaInvalid);
                }
                var result = _personService.LoginPerson(request);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
