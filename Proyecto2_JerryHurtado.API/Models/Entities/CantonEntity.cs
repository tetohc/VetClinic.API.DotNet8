namespace Proyecto2_JerryHurtado.API.Models.Entities
{
    public class CantonEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int ProvinceId { get; set; }

        public virtual ICollection<CustomerEntity> Customer { get; set; } = new List<CustomerEntity>();

        public virtual ICollection<DistrictEntity> District { get; set; } = new List<DistrictEntity>();

        public virtual ProvinceEntity Province { get; set; } = null!;
    }
}