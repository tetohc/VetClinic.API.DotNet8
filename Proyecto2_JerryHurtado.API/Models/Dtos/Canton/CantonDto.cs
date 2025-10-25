namespace Proyecto2_JerryHurtado.API.Models.Dtos.Canton
{
    public class CantonDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ProvinceId { get; set; }
    }
}