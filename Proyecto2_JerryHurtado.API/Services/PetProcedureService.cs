using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Mappers;
using Proyecto2_JerryHurtado.API.Models.Dtos.PetProcedure;
using Proyecto2_JerryHurtado.API.Models.Entities;
using Proyecto2_JerryHurtado.API.Models.Enums;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    public class PetProcedureService : IService<PetProcedureCreateDto, PetProcedureUpdateDto, PetProcedureDto>, IProcedureCascadeDeletionService
    {
        private readonly IReadOnlyPetService _petService;
        private readonly Lazy<IReadOnlyCustomerService> _customerService;

        public PetProcedureService(
            IReadOnlyPetService petService,
            Lazy<IReadOnlyCustomerService> customerService)
        {
            _petService = petService;
            _customerService = customerService;
        }

        #region Datos simulados - Procedimientos precargados

        private static readonly Guid Procedure1Id = Guid.Parse("d09988ef-5f76-4aa7-b39c-60f8026de7ee");
        private static readonly Guid Procedure2Id = Guid.Parse("ad9935a1-8535-4260-a0d3-be6953be0f2e");
        private static readonly Guid Procedure3Id = Guid.Parse("dca76a58-fe78-4691-b76d-eaac7c4d3669");

        private static readonly Guid Customer1Id = Guid.Parse("d3e4bd42-7acb-45bb-b4d7-2d2d5cc75ace");
        private static readonly Guid Customer2Id = Guid.Parse("714ee094-735d-47b2-af3a-c9be3ea2cf6b");
        private static readonly Guid Customer3Id = Guid.Parse("ec5cc7df-d594-498c-ac8b-a330f1dc791f");

        private static readonly Guid Pet1Id = Guid.Parse("4a43b032-9d5d-45cb-9000-4b1709290664");
        private static readonly Guid Pet2Id = Guid.Parse("77636d32-e7dc-4305-8f29-47653dfc3d72");
        private static readonly Guid Pet3Id = Guid.Parse("5da2eb06-8907-4354-b981-0fa54f062465");
        private static readonly Guid Pet4Id = Guid.Parse("b585e1b1-e7e0-48e9-9173-acf8f134da0c");
        private static readonly Guid Pet5Id = Guid.Parse("47c34923-b693-407d-938c-f945ce5e7a15");

        private readonly List<PetProcedureEntity> _entities = new()
        {
            new PetProcedureEntity
            {
                Id = Procedure1Id,
                CustomerId = Customer1Id,
                PetId = Pet1Id,
                ProcedureTypeId = 2,
                Status = 1,
                Customer = new CustomerEntity
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
                },
                Pet = new PetEntity
                {
                    Id = Pet1Id,
                    CustomerId = Customer1Id,
                    PetSpecies = (int)PetSpecies.Dog,
                    Name = "Sombra",
                    Race = "Labrador",
                    Age = 3,
                    Color = "Negro",
                    LastVisitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(-10))
                }
            },
            new PetProcedureEntity
            {
                Id = Procedure2Id,
                CustomerId = Customer1Id,
                PetId = Pet2Id,
                ProcedureTypeId = 3,
                Status = 2,
                Customer = new CustomerEntity
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
                },
                Pet = new PetEntity
                {
                    Id = Pet2Id,
                    CustomerId = Customer1Id,
                    PetSpecies = (int)PetSpecies.Cat,
                    Name = "Nube",
                    Race = "Siames",
                    Age = 2,
                    Color = "Gris",
                    LastVisitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(-6))
                }
            },
            new PetProcedureEntity
            {
                Id = Procedure3Id,
                CustomerId = Customer2Id,
                PetId = Pet3Id,
                ProcedureTypeId = 10,
                Status = 3,
                Customer = new CustomerEntity
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
                },
                Pet = new PetEntity
                {
                    Id = Pet3Id,
                    CustomerId = Customer2Id,
                    PetSpecies = (int)PetSpecies.Dog,
                    Name = "Ceniza",
                    Race = "Poodle",
                    Age = 5,
                    Color = "Blanco",
                    LastVisitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(-8))
                }
            },
        };

        #endregion Datos simulados - Procedimientos precargados

        #region CRUD

        public bool Create(PetProcedureCreateDto createDto)
        {
            try
            {
                PetProcedureEntity entity = createDto.FromCreateDtoToEntity();
                CustomerEntity? customer = _customerService.Value.GetById(createDto.CustomerId)?.ToEntity();
                PetEntity? pet = _petService.GetById(createDto.PetId)?.ToEntity();

                if (customer is null || pet is null)
                    return false;

                entity.Customer = customer;
                entity.Pet = pet;
                _entities.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(PetProcedureUpdateDto updateDto)
        {
            try
            {
                var existingEntity = _entities.FirstOrDefault(x => x.Id == updateDto.Id);
                if (existingEntity is null)
                    return false;

                existingEntity.ProcedureTypeId = updateDto.ProcedureTypeId;
                existingEntity.Status = updateDto.Status;
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
            return _entities.Remove(entityToRemove);
        }

        public void DeleteProceduresByCustomerId(Guid customerId)
        {
            _entities.RemoveAll(x => x.CustomerId == customerId);
        }

        public void DeleteProceduresByPetId(Guid petId)
        {
            _entities.RemoveAll(x => x.PetId == petId);
        }

        public PetProcedureDto? GetById(Guid id) => _entities.FirstOrDefault(x => x.Id == id)?.ToDto();

        public List<PetProcedureDto> GetAll() => _entities.ToDtosList();

        public List<PetProcedureDto> Search(string query)
        {
            return _entities
                .Where(x => x.Customer.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            x.Pet.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            EnumExtensions.GetDisplayName((PetProcedureStatus)x.Status).Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToDtosList();
        }

        public int Count() => _entities.Count;

        #endregion CRUD
    }
}