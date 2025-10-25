using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Models.Dtos.PetProcedure;
using Proyecto2_JerryHurtado.API.Models.Responses;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Controllers
{
    [Route("pets-procedures")]
    [Produces("application/json")]
    [ApiController]
    public class PetProceduresController : ControllerBase
    {
        private readonly IService<PetProcedureCreateDto, PetProcedureUpdateDto, PetProcedureDto> _service;

        public PetProceduresController(IService<PetProcedureCreateDto, PetProcedureUpdateDto, PetProcedureDto> service)
        {
            _service = service;
        }

        #region Operaciones de Escritura (CUD)

        /// <summary>
        /// Crea un nuevo procedimiento de mascota en el sistema.
        /// </summary>
        /// <param name="petProcedureCreateDto">Datos del procedimiento de mascota a registrar.</param>
        /// <param name="validator">Validador de reglas para el DTO de creación.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<PetProcedureCreateDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<object>))]
        public IActionResult Create([FromBody] PetProcedureCreateDto petProcedureCreateDto,
            [FromServices] IValidator<PetProcedureCreateDto> validator)
        {
            var result = CrudHelper.Create(_service, petProcedureCreateDto, validator);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Actualiza los datos de un procedimiento de mascota existente en el sistema.
        /// </summary>
        /// <param name="petProcedureUpdateDto">Datos actualizados del procedimiento de mascota.</param>
        /// <param name="id">Identificador único del procedimiento de mascota a modificar.</param>
        /// <param name="validator">Validador de reglas para el DTO de actualización.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PetProcedureUpdateDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<object>))]
        public IActionResult Update([FromBody] PetProcedureUpdateDto petProcedureUpdateDto,
            [FromRoute] Guid id,
            [FromServices] IValidator<PetProcedureUpdateDto> validator)
        {
            var result = CrudHelper.Update(_service, petProcedureUpdateDto, id, validator);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Elimina un procedimiento de mascota del sistema según su identificador único.
        /// </summary>
        /// <param name="id">Identificador del procedimiento de mascota a eliminar.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PetProcedureDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<object>))]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var result = CrudHelper.Delete(_service, id);
            return StatusCode(result.StatusCode, result);
        }

        #endregion Operaciones de Escritura (CUD)

        #region Operaciones de Lectura (R)

        /// <summary>
        /// Obtiene los datos de un procedimiento de mascota por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del procedimiento de mascota a consultar.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PetProcedureDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<object>))]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = CrudHelper.GetById(_service, id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Obtiene todos los procedimientos de mascotas registrados en el sistema o filtra por coincidencia si se proporciona un término de búsqueda.
        /// </summary>
        /// <param name="query">
        /// Texto opcional para buscar coincidencias en los datos de los procedimientos de mascotas.
        /// Si se omite, se devuelve la lista completa de procedimientos de mascotas registrados.
        /// </param>
        /// <returns>
        /// Respuesta HTTP con una lista de  los procedimientos de mascotas disponibles o filtrados.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<PetProcedureDto>>), StatusCodes.Status200OK)]
        public IActionResult GetAll([FromQuery] string? query)
        {
            var result = string.IsNullOrWhiteSpace(query)
                ? CrudHelper.GetList(_service)
                : CrudHelper.Search(_service, query);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Obtiene la cantidad total de procedimientos de mascotas registrados en el sistema.
        /// </summary>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpGet("Count")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public IActionResult Count()
        {
            var result = CrudHelper.Count(_service);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        #endregion Operaciones de Lectura (R)
    }
}