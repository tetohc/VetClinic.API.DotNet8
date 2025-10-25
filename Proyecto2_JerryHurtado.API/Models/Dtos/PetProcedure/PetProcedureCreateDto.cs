namespace Proyecto2_JerryHurtado.API.Models.Dtos.PetProcedure
{
    public class PetProcedureCreateDto
    {
        public Guid CustomerId { get; set; }
        public Guid PetId { get; set; }
        public int ProcedureTypeId { get; set; }
        public int Status { get; set; }
    }
}