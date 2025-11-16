using Microsoft.EntityFrameworkCore;
using Proyecto2_JerryHurtado.API.Database;
using Proyecto2_JerryHurtado.API.Mappers;
using Proyecto2_JerryHurtado.API.Models.Dtos.ProcedureType;
using Proyecto2_JerryHurtado.API.Models.Dtos.Report;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    /// <summary>
    /// Servicio para generar reportes de proyección de vacunación.
    /// </summary>
    public class ReportService : IVaccinationAnnualService
    {
        private readonly DatabaseContext _databaseContext;

        public ReportService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<ReportDto>> GetVaccinationsDueNextWeek()
        {
            var petProcedures = await _databaseContext.PetProcedure
                .Include(x => x.Customer)
                .Include(x => x.Pet)
                .ToListAsync();

            int annualVaccinationProcedureTypeId = 14;
            var procedureTypeAnnualVaccination = petProcedures
                .Where(x => x.ProcedureTypeId == annualVaccinationProcedureTypeId)
                .Select(x => x.ToDto())
                .ToList();

            var petsDueForVaccination = procedureTypeAnnualVaccination.Where(x => x.Pet.IsNextVisitInNextWeek()).ToList();
            var report = petsDueForVaccination
                .Select(procedure => new ReportDto
                {
                    Customer = procedure.Customer,
                    Pet = procedure.Pet,
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