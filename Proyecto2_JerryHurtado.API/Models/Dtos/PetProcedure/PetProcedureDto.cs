using Proyecto2_JerryHurtado.API.Models.Dtos.Customer;
using Proyecto2_JerryHurtado.API.Models.Dtos.Pet;

namespace Proyecto2_JerryHurtado.API.Models.Dtos.PetProcedure
{
    public class PetProcedureDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid PetId { get; set; }
        public int ProcedureTypeId { get; set; }
        public int Status { get; set; }

        public CustomerDto Customer { get; set; } = null!;
        public PetSimpleDto Pet { get; set; } = null!;
    }
}