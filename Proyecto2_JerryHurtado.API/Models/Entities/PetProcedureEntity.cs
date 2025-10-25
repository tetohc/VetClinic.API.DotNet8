namespace Proyecto2_JerryHurtado.API.Models.Entities
{
    public class PetProcedureEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid PetId { get; set; }
        public int ProcedureTypeId { get; set; }
        public int Status { get; set; }

        public CustomerEntity Customer { get; set; } = null!;
        public PetEntity Pet { get; set; } = null!;
        public ProcedureTypeEntity ProcedureType { get; set; } = null!;
    }
}