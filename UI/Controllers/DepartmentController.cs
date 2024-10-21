using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biokudi_Backend.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController(IDepartmentService _departmentService) : ControllerBase
    {
        private readonly IDepartmentService _departmentService = _departmentService;

        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> Get()
        {
            var result = await _departmentService.GetDepartments();
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null || !result.Value.Any())
                return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _departmentService.GetDepartmentById(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] DepartmentRequestDto department)
        {
            var result = await _departmentService.CreateDepartment(department);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] DepartmentRequestDto department)
        {
            var result = await _departmentService.UpdateDepartment(id, department);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _departmentService.DeleteDepartment(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }
    }
}
