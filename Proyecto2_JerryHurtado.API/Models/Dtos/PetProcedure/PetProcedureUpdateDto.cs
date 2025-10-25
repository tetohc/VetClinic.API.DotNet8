namespace Proyecto2_JerryHurtado.API.Models.Dtos.PetProcedure
{
    public class PetProcedureUpdateDto
    {
        public Guid Id { get; set; }
        public int ProcedureTypeId { get; set; }
        public int Status { get; set; }
    }
}