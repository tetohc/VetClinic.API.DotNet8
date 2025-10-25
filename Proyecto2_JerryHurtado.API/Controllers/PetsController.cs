using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Models.Dtos.Pet;
using Proyecto2_JerryHurtado.API.Models.Responses;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Controllers
{
    [Route("pets")]
    [Produces("application/json")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IService<PetCreateDto, PetUpdateDto, PetDto> _service;

        public PetsController(IService<PetCreateDto, PetUpdateDto, PetDto> service)
        {
            _service = service;
        }

        #region Operaciones de Escritura (CUD)

        /// <summary>
        /// Crea una nueva mascota en el sistema.
        /// </summary>
        /// <param name="petCreateDto">Datos de la mascota a registrar.</param>
        /// <param name="validator">Validador de reglas para el DTO de creación.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<PetCreateDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<object>))]
        public IActionResult Create([FromBody] PetCreateDto petCreateDto,
            [FromServices] IValidator<PetCreateDto> validator)
        {
            var result = CrudHelper.Create(_service, petCreateDto, validator);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Actualiza los datos de una mascota existente en el sistema.
        /// </summary>
        /// <param name="petUpdateDto">Datos actualizados de la mascota.</param>
        /// <param name="id">Identificador único de la mascota a modificar</param>
        /// <param name="validator">Validador de reglas para el DTO de actualización.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PetUpdateDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<object>))]
        public IActionResult Update([FromBody] PetUpdateDto petUpdateDto,
            [FromRoute] Guid id,
            [FromServices] IValidator<PetUpdateDto> validator)
        {
            var result = CrudHelper.Update(_service, petUpdateDto, id, validator);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Elimina una mascota del sistema según su identificador único.
        /// </summary>
        /// <param name="id">Identificador de la mascota a eliminar.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PetDto>))]
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
        /// Obtiene los datos de una mascota por su identificador único.
        /// </summary>
        /// <param name="id">Identificador de la mascota a consultar.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PetDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<object>))]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = CrudHelper.GetById(_service, id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Obtiene todas las mascotas registradas en el sistema o filtra por coincidencia si se proporciona un término de búsqueda.
        /// </summary>
        /// <param name="query">
        /// Texto opcional para buscar coincidencias en los datos de las mascotas.
        /// Si se omite, se devuelve la lista completa de mascotas registradas.
        /// </param>
        /// <returns>
        /// Respuesta HTTP con una lista de  las mascotas disponibles o filtradas.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<PetDto>>), StatusCodes.Status200OK)]
        public IActionResult GetAll([FromQuery] string? query)
        {
            var result = string.IsNullOrWhiteSpace(query)
                ? CrudHelper.GetList(_service)
                : CrudHelper.Search(_service, query);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Obtiene la cantidad total de mascotas registradas en el sistema.
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