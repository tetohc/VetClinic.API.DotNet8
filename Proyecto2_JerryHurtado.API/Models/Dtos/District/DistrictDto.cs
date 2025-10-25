namespace Proyecto2_JerryHurtado.API.Models.Dtos.District
{
    public class DistrictDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CantonId { get; set; }
    }
}