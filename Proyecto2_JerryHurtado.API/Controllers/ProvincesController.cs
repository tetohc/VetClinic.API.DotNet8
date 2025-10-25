using Microsoft.AspNetCore.Mvc;
using Proyecto2_JerryHurtado.API.Factories;
using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Models.Dtos.Province;
using Proyecto2_JerryHurtado.API.Models.Enums;
using Proyecto2_JerryHurtado.API.Models.Responses;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Controllers
{
    [Route("provinces")]
    [Produces("application/json")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly IGetAllService<ProvinceDto> _service;

        public ProvincesController(IGetAllService<ProvinceDto> service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todas las provincias registradas en el sistema.
        /// </summary>
        /// <returns>Una lista con las provincias disponibles.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<ProvinceDto>>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var data = _service.GetAll();
            return StatusCode(
                StatusCodes.Status200OK,
                ApiResponseFactory.Success(
                    statusCode: StatusCodes.Status200OK,
                    data,
                    message: ApiResponseMessage.Ok200.GetDisplayName()
                )
            );
        }
    }
}