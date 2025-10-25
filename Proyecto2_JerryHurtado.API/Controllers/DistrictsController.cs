using Microsoft.AspNetCore.Mvc;
using Proyecto2_JerryHurtado.API.Factories;
using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Models.Dtos.District;
using Proyecto2_JerryHurtado.API.Models.Dtos.Shared;
using Proyecto2_JerryHurtado.API.Models.Enums;
using Proyecto2_JerryHurtado.API.Models.Responses;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Controllers
{
    [Route("districts")]
    [Produces("application/json")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly IGetAllByParentService<DistrictDto> _service;

        public DistrictsController(IGetAllByParentService<DistrictDto> service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todos los distritos registrados en el sistema filtrados por canton.
        /// </summary>
        /// <param name="id">Id de cantón al que pertenece el distrito.</param>
        /// <returns>Una lista con los distritos disponibles.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<List<SelectListItemDto<int>>>), StatusCodes.Status200OK)]
        public IActionResult GetByCanton(int id)
        {
            var data = _service.GetAllById(id);
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