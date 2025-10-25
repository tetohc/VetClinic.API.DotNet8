namespace Proyecto2_JerryHurtado.API.Models.Entities
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string PersonalIdNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int ProvinceId { get; set; }
        public int CantonId { get; set; }
        public int DistrictId { get; set; }
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int ContactPreference { get; set; }

        public ProvinceEntity Province { get; set; } = null!;
        public CantonEntity Canton { get; set; } = null!;
        public DistrictEntity District { get; set; } = null!;
        public List<PetEntity> Pets { get; set; } = new();
        public List<PetProcedureEntity> PetProcedures { get; set; } = new();
    }
}