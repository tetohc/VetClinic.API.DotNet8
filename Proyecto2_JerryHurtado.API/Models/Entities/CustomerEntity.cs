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

        public virtual CantonEntity Canton { get; set; } = null!;
        public virtual DistrictEntity District { get; set; } = null!;
        public virtual ICollection<PetEntity> Pet { get; set; } = new List<PetEntity>();
        public virtual ICollection<PetProcedureEntity> PetProcedure { get; set; } = new List<PetProcedureEntity>();
        public virtual ProvinceEntity Province { get; set; } = null!;
    }
}