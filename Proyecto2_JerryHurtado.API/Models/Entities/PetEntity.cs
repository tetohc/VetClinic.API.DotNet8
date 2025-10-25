namespace Proyecto2_JerryHurtado.API.Models.Entities
{
    public class PetEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int PetSpecies { get; set; }
        public string Name { get; set; } = null!;
        public string Race { get; set; } = null!;
        public int Age { get; set; }
        public string Color { get; set; } = null!;
        public DateOnly LastVisitDate { get; set; }

        public CustomerEntity Customer { get; set; } = null!;
        public List<PetProcedureEntity> PetProcedures { get; set; } = new();
    }
}