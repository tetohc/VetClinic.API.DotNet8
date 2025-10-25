namespace Proyecto2_JerryHurtado.API.Models.Dtos.ProcedureType
{
    public class ProcedureTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }
    }
}