namespace Proyecto2_JerryHurtado.API.Models.Entities
{
    public class DistrictEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CantonId { get; set; }

        public virtual CantonEntity Canton { get; set; } = null!;

        public virtual ICollection<CustomerEntity> Customer { get; set; } = new List<CustomerEntity>();
    }
}