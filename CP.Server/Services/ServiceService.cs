using CP.Server.Repositories;
using CP.Shared.DTOs;
using CP.Shared.Entities;

namespace CP.Server.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            var services = await _repository.GetAllAsync();
            return services.Select(MapToDto);
        }

        public async Task<IEnumerable<ServiceDto>> GetByBusinessIdAsync(Guid businessId)
        {
            var services = await _repository.GetByBusinessIdAsync(businessId);
            return services.Select(MapToDto);
        }

        public async Task<IEnumerable<ServiceDto>> GetActiveAsync()
        {
            var services = await _repository.GetAllAsync();
            return services.Where(s => s.IsActive).Select(MapToDto);
        }

        public async Task<ServiceDto?> GetByIdAsync(Guid id)
        {
            var service = await _repository.GetByIdAsync(id);
            return service == null ? null : MapToDto(service);
        }

        public async Task<ServiceDto> CreateAsync(CreateServiceDto createDto)
        {
            var service = new Service
            {
                Name = createDto.Name,
                Description = createDto.Description,
                Price = createDto.Price,
                DurationMinutes = createDto.DurationMinutes,
                BusinessId = createDto.BusinessId,
                IsActive = true
            };
            var result = await _repository.CreateAsync(service);
            return MapToDto(result);
        }

        public async Task<ServiceDto?> UpdateAsync(Guid id, UpdateServiceDto updateDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = updateDto.Name;
            existing.Description = updateDto.Description;
            existing.Price = updateDto.Price;
            existing.DurationMinutes = updateDto.DurationMinutes;
            existing.IsActive = updateDto.IsActive;

            await _repository.UpdateAsync(existing);
            return MapToDto(existing);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        private static ServiceDto MapToDto(Service service)
        {
            return new ServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                DurationMinutes = service.DurationMinutes,
                IsActive = service.IsActive,
                BusinessId = service.BusinessId
            };
        }
    }
}
