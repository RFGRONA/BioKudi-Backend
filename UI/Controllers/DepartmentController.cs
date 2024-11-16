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

        /// <summary>
        /// Obtiene la lista de departamentos.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, Editor")]
        [ProducesResponseType(typeof(List<DepartmentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _departmentService.GetDepartments();
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null || !result.Value.Any())
                return NotFound();
            return Ok(result.Value);
        }

        /// <summary>
        /// Obtiene un departamento específico por su ID.
        /// </summary>
        /// <param name="id">El ID del departamento a obtener.</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _departmentService.GetDepartmentById(id);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            if (result.Value == null)
                return NotFound();
            return Ok(result.Value);
        }

        /// <summary>
        /// Crea un nuevo departamento.
        /// </summary>
        /// <param name="department">Datos del departamento a crear.</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] DepartmentRequestDto department)
        {
            var result = await _departmentService.CreateDepartment(department);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        /// <summary>
        /// Actualiza los datos de un departamento.
        /// </summary>
        /// <param name="id">El ID del departamento a actualizar.</param>
        /// <param name="department">Datos actualizados del departamento.</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] DepartmentRequestDto department)
        {
            var result = await _departmentService.UpdateDepartment(id, department);
            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);
            return Ok();
        }

        /// <summary>
        /// Elimina un departamento por su ID.
        /// </summary>
        /// <param name="id">El ID del departamento a eliminar.</param>
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