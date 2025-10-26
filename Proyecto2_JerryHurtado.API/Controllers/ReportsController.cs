using Microsoft.AspNetCore.Mvc;
using Proyecto2_JerryHurtado.API.Factories;
using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Models.Dtos.Report;
using Proyecto2_JerryHurtado.API.Models.Enums;
using Proyecto2_JerryHurtado.API.Models.Responses;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Controllers
{
    [Route("reports")]
    [Produces("application/json")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IVaccinationAnnualService _service;

        public ReportsController(IVaccinationAnnualService vaccinationReportService)
        {
            _service = vaccinationReportService;
        }

        /// <summary>
        /// Obtiene un reporte proyectado de vacunación anual para mascotas cuya próxima visita corresponde a la semana siguiente.
        /// </summary>
        /// <remarks>
        /// Este endpoint genera un reporte basado en la fecha de la última visita de cada mascota.
        /// Se proyecta el procedimiento de vacunación anual (ID = 14) para aquellas cuya próxima visita anual cae dentro de los próximos 7 días.
        /// </remarks>
        /// <returns>
        /// Una respuesta HTTP 200 con una lista de objetos <see cref="ReportDto"/> que contienen información del cliente, la mascota y el procedimiento proyectado.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<ReportDto>>), StatusCodes.Status200OK)]
        public IActionResult GetVaccinationsDueNextWeek()
        {
            var data = _service.GetVaccinationsDueNextWeek();
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