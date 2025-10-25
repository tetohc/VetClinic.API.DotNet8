namespace Proyecto2_JerryHurtado.API.Models.Dtos.Customer
{
    public class CustomerCreateDto
    {
        public string PersonalIdNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int ProvinceId { get; set; }
        public int CantonId { get; set; }
        public int DistrictId { get; set; }
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int ContactPreference { get; set; }
    }
}