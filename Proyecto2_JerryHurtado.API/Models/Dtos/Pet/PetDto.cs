using Proyecto2_JerryHurtado.API.Models.Dtos.Customer;

namespace Proyecto2_JerryHurtado.API.Models.Dtos.Pet
{
    public class PetDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int PetSpecies { get; set; }
        public string Name { get; set; } = null!;
        public string Race { get; set; } = null!;
        public int Age { get; set; }
        public string Color { get; set; } = null!;
        public DateOnly LastVisitDate { get; set; }

        public CustomerDto Owner { get; set; } = null!;
    }
}