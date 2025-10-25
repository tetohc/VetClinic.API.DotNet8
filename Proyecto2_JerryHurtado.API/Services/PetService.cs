using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Mappers;
using Proyecto2_JerryHurtado.API.Models.Dtos.Customer;
using Proyecto2_JerryHurtado.API.Models.Dtos.Pet;
using Proyecto2_JerryHurtado.API.Models.Entities;
using Proyecto2_JerryHurtado.API.Models.Enums;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    /// <summary>
    /// Servicio para gestionar operaciones relacionadas con mascotas.
    /// </summary>
    public class PetService : IService<PetCreateDto, PetUpdateDto, PetDto>, IPetCascadeDeletionService, IReadOnlyPetService
    {
        private readonly Lazy<IProcedureCascadeDeletionService> _procedureCascadeDeletionService;
        private readonly Lazy<IReadOnlyCustomerService> _customerService;

        public PetService(Lazy<IProcedureCascadeDeletionService> procedureCascadeDeletionService,
            Lazy<IReadOnlyCustomerService> customerService)
        {
            _procedureCascadeDeletionService = procedureCascadeDeletionService;
            _customerService = customerService;
        }

        #region Datos simulados - Mascotas precargadas

        private static readonly Guid Pet1Id = Guid.Parse("4a43b032-9d5d-45cb-9000-4b1709290664");
        private static readonly Guid Pet2Id = Guid.Parse("77636d32-e7dc-4305-8f29-47653dfc3d72");
        private static readonly Guid Pet3Id = Guid.Parse("5da2eb06-8907-4354-b981-0fa54f062465");
        private static readonly Guid Pet4Id = Guid.Parse("b585e1b1-e7e0-48e9-9173-acf8f134da0c");
        private static readonly Guid Pet5Id = Guid.Parse("47c34923-b693-407d-938c-f945ce5e7a15");

        private static readonly Guid Customer1Id = Guid.Parse("d3e4bd42-7acb-45bb-b4d7-2d2d5cc75ace");
        private static readonly Guid Customer2Id = Guid.Parse("714ee094-735d-47b2-af3a-c9be3ea2cf6b");
        private static readonly Guid Customer3Id = Guid.Parse("ec5cc7df-d594-498c-ac8b-a330f1dc791f");

        private static readonly List<PetEntity> _entities = new()
        {
            new PetEntity
            {
                Id = Pet1Id,
                CustomerId = Customer1Id,
                PetSpecies = (int)PetSpecies.Dog,
                Name = "Sombra",
                Race = "Labrador",
                Age = 3,
                Color = "Negro",
                LastVisitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(-10)),
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
                }
            },
            new PetEntity
            {
                Id = Pet2Id,
                CustomerId = Customer1Id,
                PetSpecies = (int)PetSpecies.Cat,
                Name = "Nube",
                Race = "Siames",
                Age = 2,
                Color = "Gris",
                LastVisitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(-6)),
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
                }
            },
            new PetEntity
            {
                Id = Pet3Id,
                CustomerId = Customer2Id,
                PetSpecies = (int)PetSpecies.Dog,
                Name = "Ceniza",
                Race = "Poodle",
                Age = 5,
                Color = "Blanco",
                LastVisitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(-8)),
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
                    ContactPreference = 1
                }
            },
            new PetEntity
            {
                Id = Pet4Id,
                CustomerId = Customer3Id,
                PetSpecies = (int)PetSpecies.Rabbit,
                Name = "Copito",
                Race = "Mini Lop",
                Age = 1,
                Color = "Blanco",
                LastVisitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(-12)),
                Customer = new CustomerEntity
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
                    ContactPreference = 1
                }
            },
            new PetEntity
            {
                Id = Pet5Id,
                CustomerId = Customer3Id,
                PetSpecies = (int)PetSpecies.Other,
                Name = "Kiwi",
                Race = "Periquito Australiano",
                Age = 2,
                Color = "Verde y amarillo",
                LastVisitDate = DateOnly.FromDateTime(DateTime.Today),
                Customer = new CustomerEntity
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
                    ContactPreference = 1
                }
            }
        };

        #endregion Datos simulados - Mascotas precargadas

        #region CRUD

        public bool Create(PetCreateDto createDto)
        {
            try
            {
                PetEntity entity = createDto.FromCreateDtoToEntity();
                CustomerDto? owner = _customerService.Value.GetById(createDto.CustomerId);
                if (owner is null)
                    return false;

                entity.Customer = owner.ToEntity();
                _entities.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(PetUpdateDto updateDto)
        {
            try
            {
                var existingEntity = _entities.FirstOrDefault(x => x.Id == updateDto.Id);
                if (existingEntity is null)
                    return false;

                existingEntity.PetSpecies = updateDto.PetSpecies;
                existingEntity.Race = updateDto.Race.Trim();
                existingEntity.Age = updateDto.Age;
                existingEntity.Color = updateDto.Color.Trim();
                existingEntity.LastVisitDate = updateDto.LastVisitDate;
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
                _procedureCascadeDeletionService.Value.DeleteProceduresByPetId(entityToRemove.Id);

            return entityDeleted;
        }

        public void DeletePetsByCustomerId(Guid customerId)
        {
            _entities.RemoveAll(x => x.CustomerId == customerId);
        }

        public PetDto? GetById(Guid id) => _entities.FirstOrDefault(x => x.Id == id)?.ToDto();

        public List<PetDto> GetAll() => _entities.ToDtosList();

        public List<PetDto> Search(string query)
        {
            return _entities
                .Where(x => EnumExtensions.GetDisplayName((PetSpecies)x.PetSpecies).Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    x.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    x.Race.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    x.Color.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToDtosList();
        }

        public int Count() => _entities.Count;

        #endregion CRUD
    }
}