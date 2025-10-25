namespace Proyecto2_JerryHurtado.API.Models.Entities
{
    public class ProvinceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<CantonEntity> Cantons { get; set; } = new();
    }
}