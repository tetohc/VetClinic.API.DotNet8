namespace Proyecto2_JerryHurtado.API.Models.Entities
{
    public class CantonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ProvinceId { get; set; }

        public ProvinceEntity Province { get; set; } = null!;
        public List<DistrictEntity> Districts { get; set; } = new();
    }
}