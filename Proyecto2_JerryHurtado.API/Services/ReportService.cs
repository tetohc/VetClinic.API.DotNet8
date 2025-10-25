using Proyecto2_JerryHurtado.API.Models.Dtos.ProcedureType;
using Proyecto2_JerryHurtado.API.Models.Dtos.Report;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    /// <summary>
    /// Servicio para generar reportes de proyección de vacunación.
    /// </summary>
    public class ReportService : IProjectedVaccinationReportService
    {
        private readonly IReadOnlyCustomerService _customerService;
        private readonly IReadOnlyPetService _petService;

        public ReportService(
            IReadOnlyCustomerService readOnlyCustomerService,
            IReadOnlyPetService readOnlyPetService)
        {
            _customerService = readOnlyCustomerService;
            _petService = readOnlyPetService;
        }

        public List<ReportDto> GetProjectedVaccinationReports()
        {
            var pets = _petService.GetAll();
            var customers = _customerService.GetAll();

            var petsDueForVaccination = pets.Where(x => x.IsNextVisitInNextWeek()).ToList();
            var report = petsDueForVaccination
                .Select(pet => new ReportDto
                {
                    Customer = customers.FirstOrDefault(x => x.Id == pet.CustomerId)!,
                    Pet = pet,
                    ProjectedProcedure = new ProcedureTypeDto
                    {
                        Id = 14,
                        Name = "Vacunas Anuales de mascota",
                        Description = "Vacunas anuales.",
                        Price = 40000
                    }
                })
                .ToList();

            return report ?? new List<ReportDto>();
        }
    }
}