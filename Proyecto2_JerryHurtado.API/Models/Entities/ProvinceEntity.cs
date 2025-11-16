namespace Proyecto2_JerryHurtado.API.Models.Entities
{
    public class ProvinceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CantonEntity> Canton { get; set; } = new List<CantonEntity>();
        public virtual ICollection<CustomerEntity> Customer { get; set; } = new List<CustomerEntity>();
    }
}