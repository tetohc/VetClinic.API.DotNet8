using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Models.Dtos.Customer;
using Proyecto2_JerryHurtado.API.Models.Responses;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Controllers
{
    [Route("customers")]
    [Produces("application/json")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IService<CustomerCreateDto, CustomerUpdateDto, CustomerDto> _service;

        public CustomersController(IService<CustomerCreateDto, CustomerUpdateDto, CustomerDto> service)
        {
            _service = service;
        }

        #region Operaciones de Escritura (CUD)

        /// <summary>
        /// Crea un nuevo cliente en el sistema.
        /// </summary>
        /// <param name="customerCreateDto">Datos del cliente a registrar.</param>
        /// <param name="validator">Validador de reglas para el DTO de creación.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<CustomerCreateDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<object>))]
        public IActionResult Create([FromBody] CustomerCreateDto customerCreateDto,
            [FromServices] IValidator<CustomerCreateDto> validator)
        {
            var result = CrudHelper.Create(_service, customerCreateDto, validator);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Actualiza los datos de un cliente existente en el sistema.
        /// </summary>
        /// <param name="customerUpdateDto">Datos actualizados del cliente.</param>
        /// <param name="id">Identificador único del cliente a modificar.</param>
        /// <param name="validator">Validador de reglas para el DTO de actualización.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<CustomerUpdateDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<object>))]
        public IActionResult Update([FromBody] CustomerUpdateDto customerUpdateDto,
            [FromRoute] Guid id,
            [FromServices] IValidator<CustomerUpdateDto> validator)
        {
            var result = CrudHelper.Update(_service, customerUpdateDto, id, validator);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Elimina un cliente del sistema según su identificador único.
        /// </summary>
        /// <param name="id">Identificador del cliente a eliminar.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<CustomerDto>))]
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
        /// Obtiene los datos de un cliente por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del cliente a consultar.</param>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<CustomerDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<object>))]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = CrudHelper.GetById(_service, id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Obtiene todos los clientes registrados en el sistema o filtra por coincidencia si se proporciona un término de búsqueda.
        /// </summary>
        /// <param name="query">
        /// Texto opcional para buscar coincidencias en los datos de los clientes.
        /// Si se omite, se devuelve la lista completa de clientes registrados.
        /// </param>
        /// <returns>
        /// Respuesta HTTP con una lista de  los clientes disponibles o filtrados.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<CustomerDto>>), StatusCodes.Status200OK)]
        public IActionResult GetAll([FromQuery] string? query)
        {
            var result = string.IsNullOrWhiteSpace(query)
                ? CrudHelper.GetList(_service)
                : CrudHelper.Search(_service, query);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Obtiene la cantidad total de clientes registrados en el sistema.
        /// </summary>
        /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
        [HttpGet("count")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public IActionResult Count()
        {
            var result = CrudHelper.Count(_service);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        #endregion Operaciones de Lectura (R)
    }
}