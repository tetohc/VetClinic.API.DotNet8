using Proyecto2_JerryHurtado.API.Mappers;
using Proyecto2_JerryHurtado.API.Models.Dtos.Canton;
using Proyecto2_JerryHurtado.API.Models.Dtos.Customer;
using Proyecto2_JerryHurtado.API.Models.Dtos.District;
using Proyecto2_JerryHurtado.API.Models.Dtos.Province;
using Proyecto2_JerryHurtado.API.Models.Entities;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    /// <summary>
    /// Servicio para gestionar operaciones relacionadas con clientes.
    /// </summary>
    public class CustomerService : IService<CustomerCreateDto, CustomerUpdateDto, CustomerDto>, IReadOnlyCustomerService
    {
        private readonly IGetAllByParentService<CantonDto> _cantonService;
        private readonly IGetAllByParentService<DistrictDto> _districtService;
        private readonly IGetAllService<ProvinceDto> _provinceService;
        private readonly IPetCascadeDeletionService _petCascadeDeletionService;
        private readonly Lazy<IProcedureCascadeDeletionService> _procedureCascadeDeletionService;

        public CustomerService(
            IGetAllByParentService<CantonDto> cantonService,
            IGetAllByParentService<DistrictDto> districtService,
            IGetAllService<ProvinceDto> provinceService,
            IPetCascadeDeletionService petCascadeDeletionService,
            Lazy<IProcedureCascadeDeletionService> procedureCascadeDeletionService)
        {
            _cantonService = cantonService;
            _districtService = districtService;
            _provinceService = provinceService;
            _petCascadeDeletionService = petCascadeDeletionService;
            _procedureCascadeDeletionService = procedureCascadeDeletionService;
        }

        #region Datos simulados - Clientes precargados

        private static readonly Guid Customer1Id = Guid.Parse("d3e4bd42-7acb-45bb-b4d7-2d2d5cc75ace");
        private static readonly Guid Customer2Id = Guid.Parse("714ee094-735d-47b2-af3a-c9be3ea2cf6b");
        private static readonly Guid Customer3Id = Guid.Parse("ec5cc7df-d594-498c-ac8b-a330f1dc791f");

        private List<CustomerEntity> _entities = new()
        {
            new CustomerEntity
            {
                Id = Customer1Id,
                PersonalIdNumber = "1-2345-6789",
                Name = "Juan Pérez",
                ProvinceId = 1,
                CantonId = 2,
                DistrictId = 4,
                Address = "Calle 1, Casa 2",
                Email = "juan@email.com",
                PhoneNumber = "8888-8888",
                ContactPreference = 1,
                Province = new ProvinceEntity
                {
                    Id = 1,
                    Name = "San José"
                },
                Canton = new CantonEntity
                {
                    Id = 2,
                    Name = "Alajuela"
                },
                District = new DistrictEntity
                {
                    Id = 4,
                    Name = "San Rafael"
                }
            },
            new CustomerEntity
            {
                Id = Customer2Id,
                PersonalIdNumber = "2-2445-6749",
                Name = "María Guzmán",
                ProvinceId = 2,
                CantonId = 2,
                DistrictId = 4,
                Address = "Calle 1, Casa 2",
                Email = "maria@email.com",
                PhoneNumber = "8888-8888",
                ContactPreference = 1,
                Province = new ProvinceEntity
                {
                    Id = 1,
                    Name = "San José"
                },
                Canton = new CantonEntity
                {
                    Id = 2,
                    Name = "Alajuela"
                },
                District = new DistrictEntity
                {
                    Id = 4,
                    Name = "San Rafael"
                }
            },
            new CustomerEntity
            {
                Id = Customer3Id,
                PersonalIdNumber = "2-2445-6749",
                Name = "Pablo Martínez",
                ProvinceId = 2,
                CantonId = 2,
                DistrictId = 4,
                Address = "Calle 1, Casa 2",
                Email = "pablo@email.com",
                PhoneNumber = "8888-8888",
                ContactPreference = 1,
                Province = new ProvinceEntity
                {
                    Id = 2,
                    Name = "Alajuela"
                },
                Canton = new CantonEntity
                {
                    Id = 8,
                    Name = "Grecia"
                },
                District = new DistrictEntity
                {
                    Id = 22,
                    Name = "Grecia Centro"
                }
            },
        };

        #endregion Datos simulados - Clientes precargados

        #region CRUD

        public bool Create(CustomerCreateDto createDto)
        {
            try
            {
                CustomerEntity entity = createDto.FromCreateDtoToEntity();
                DistrictDto? district = _districtService.GetById(entity.DistrictId);
                CantonDto? canton = _cantonService.GetById(entity.CantonId);
                ProvinceDto? province = _provinceService.GetAll()
                    .Where(x => x.Id == entity.ProvinceId).FirstOrDefault();

                if (district != null)
                    entity.District = new DistrictEntity
                    {
                        Id = district.Id,
                        Name = district.Name,
                        CantonId = district.CantonId
                    };

                if (canton != null)
                    entity.Canton = new CantonEntity
                    {
                        Id = canton.Id,
                        Name = canton.Name,
                        ProvinceId = canton.ProvinceId,
                    };

                if (province != null)
                    entity.Province = new ProvinceEntity
                    {
                        Id = province.Id,
                        Name = province.Name
                    };

                _entities.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(CustomerUpdateDto updateDto)
        {
            try
            {
                var existingEntity = _entities.FirstOrDefault(x => x.Id == updateDto.Id);
                if (existingEntity is null)
                    return false;

                existingEntity.PersonalIdNumber = updateDto.PersonalIdNumber.Trim();
                existingEntity.Name = updateDto.Name.Trim();
                existingEntity.ProvinceId = updateDto.ProvinceId;
                existingEntity.CantonId = updateDto.CantonId;
                existingEntity.DistrictId = updateDto.DistrictId;
                existingEntity.Address = updateDto.Address.Trim();
                existingEntity.PhoneNumber = updateDto.PhoneNumber.Trim();
                existingEntity.ContactPreference = updateDto.ContactPreference;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            var entityToRemove = _entities.FirstOrDefault(x => x.Id == id);
            if (entityToRemove is null)
                return false;

            bool entityDeleted = _entities.Remove(entityToRemove);
            if (entityDeleted)
            {
                _petCascadeDeletionService.DeletePetsByCustomerId(id);
                _procedureCascadeDeletionService.Value.DeleteProceduresByCustomerId(id);
            }

            return entityDeleted;
        }

        public CustomerDto? GetById(Guid id) => _entities.FirstOrDefault(x => x.Id == id)?.ToDto();

        public List<CustomerDto> GetAll() => _entities.ToDtosList();

        public List<CustomerDto> Search(string query)
        {
            return _entities
                .Where(x => x.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            x.PersonalIdNumber.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            x.Province.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToDtosList();
        }

        public int Count() => _entities.Count;
    }

    #endregion CRUD
}