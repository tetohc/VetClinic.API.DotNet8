using Proyecto2_JerryHurtado.API.Models.Dtos.Report;

namespace Proyecto2_JerryHurtado.API.Services.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de generación de reportes de proyección de vacunación.
    /// </summary>
    public interface IProjectedVaccinationReportService
    {
        public List<ReportDto> GetProjectedVaccinationReports();
    }
}