using Proyecto2_JerryHurtado.API.Models.Dtos.Customer;
using Proyecto2_JerryHurtado.API.Models.Dtos.Pet;
using Proyecto2_JerryHurtado.API.Models.Dtos.ProcedureType;

namespace Proyecto2_JerryHurtado.API.Models.Dtos.Report
{
    public class ReportDto
    {
        public CustomerDto Customer { get; set; } = null!;
        public PetSimpleDto Pet { get; set; } = null!;
        public ProcedureTypeDto ProjectedProcedure { get; set; } = null!;
    }
}