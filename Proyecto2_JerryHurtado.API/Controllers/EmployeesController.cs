using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Models.Dtos.Employee;
using Proyecto2_JerryHurtado.API.Models.Responses;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Controllers
{
    [Route("employees")]
    [Produces("application/json")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IService<EmployeeCreateDto, EmployeeUpdateDto, EmployeeDto> _service;

        public EmployeesController(IService<EmployeeCreateDto, EmployeeUpdateDto, EmployeeDto> service)
        {
            _service = service;
        }

        #region Operaciones de Escritura (CUD)

        /// <summary>
        /// Crea un nuevo empleado en el sistema.
        /// </summary>
        /// <param name="employeeCreateDto">Datos del empleado a registrar.</param>
        /// <param name="validator">Validador de reglas para el DTO de creación.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<EmployeeCreateDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDto employeeCreateDto,
            [FromServices] IValidator<EmployeeCreateDto> validator)
        {
            var result = await CrudHelper.Create(_service, employeeCreateDto, validator);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Actualiza los datos de un empleado existente en el sistema.
        /// </summary>
        /// <param name="employeeUpdateDto">Datos actualizados del empleado.</param>
        /// <param name="id">Identificador único del empleado a modificar.</param>
        /// <param name="validator">Validador de reglas para el DTO de actualización.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeUpdateDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> Update([FromBody] EmployeeUpdateDto employeeUpdateDto,
            [FromRoute] Guid id,
            [FromServices] IValidator<EmployeeUpdateDto> validator)
        {
            var result = await CrudHelper.Update(_service, employeeUpdateDto, id, validator);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Elimina un empleado del sistema según su identificador único.
        /// </summary>
        /// <param name="id">Identificador del empleado a eliminar.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await CrudHelper.Delete(_service, id);
            return StatusCode(result.StatusCode, result);
        }

        #endregion Operaciones de Escritura (CUD)

        #region Operaciones de Lectura (R)

        /// <summary>
        /// Obtiene los datos de un empleado por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del empleado a consultar.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await CrudHelper.GetById(_service, id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Obtiene todos los empleados registrados en el sistema o filtra por coincidencia si se proporciona un término de búsqueda.
        /// </summary>
        /// <param name="query">
        /// Texto opcional para buscar coincidencias en los datos de los empleados.
        /// Si se omite, se devuelve la lista completa de empleados registrados.
        /// </param>
        /// <returns>
        /// Respuesta HTTP con una lista de  los empleados disponibles o filtrados.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<EmployeeDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var result = string.IsNullOrWhiteSpace(query)
                ? await CrudHelper.GetList(_service)
                : await CrudHelper.Search(_service, query);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Obtiene la cantidad total de empleados registrados en el sistema.
        /// </summary>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpGet("Count")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var result = await CrudHelper.Count(_service);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        #endregion Operaciones de Lectura (R)
    }
}