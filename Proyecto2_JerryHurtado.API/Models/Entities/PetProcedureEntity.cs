namespace Proyecto2_JerryHurtado.API.Models.Entities
{
    public class PetProcedureEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid PetId { get; set; }
        public int ProcedureTypeId { get; set; }
        public int Status { get; set; }

        public virtual CustomerEntity Customer { get; set; } = null!;
        public virtual PetEntity Pet { get; set; } = null!;
    }
}