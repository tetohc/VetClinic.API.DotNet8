using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto2_JerryHurtado.API.Factories;
using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Models.Dtos.Canton;
using Proyecto2_JerryHurtado.API.Models.Dtos.District;
using Proyecto2_JerryHurtado.API.Models.Dtos.Shared;
using Proyecto2_JerryHurtado.API.Models.Enums;
using Proyecto2_JerryHurtado.API.Models.Responses;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Controllers
{
    [Route("cantons")]
    [Produces("application/json")]
    [ApiController]
    public class CantonsController : ControllerBase
    {
        private readonly IGetAllByParentService<CantonDto> _service;

        public CantonsController(IGetAllByParentService<CantonDto> service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todos los cantones registrados en el sistema filtrados por provincia.
        /// </summary>
        /// <param name="id">Id de provincia al que pertenece el cantón.</param>
        /// <returns>Una lista con los cantones disponibles.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<List<SelectListItemDto<int>>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCanton(int id)
        {
            var data = await _service.GetAllById(id);
            var result = data.Select(x => new SelectListItemDto<int> { Id = x.Id, Name = x.Name }).ToList();
            return StatusCode(
                StatusCodes.Status200OK,
                ApiResponseFactory.Success(
                    statusCode: StatusCodes.Status200OK,
                    result,
                    message: ApiResponseMessage.Ok200.GetDisplayName()
                )
            );
        }
    }
}