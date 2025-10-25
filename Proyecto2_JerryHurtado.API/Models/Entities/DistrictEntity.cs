namespace Proyecto2_JerryHurtado.API.Models.Entities
{
    public class DistrictEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CantonId { get; set; }

        public CantonEntity Canton { get; set; } = null!;
    }
}