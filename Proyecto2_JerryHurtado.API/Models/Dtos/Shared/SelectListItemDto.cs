namespace Proyecto2_JerryHurtado.API.Models.Dtos.Shared
{
    public class SelectListItemDto<T>
    {
        public T Id { get; set; } = default!;
        public string Name { get; set; } = null!;
    }
}