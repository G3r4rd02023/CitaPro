using CP.Server.Repositories;
using CP.Shared.DTOs;
using CP.Shared.Entities;

namespace CP.Server.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _repository;
        private readonly IImageService _imageService;

        public BusinessService(IBusinessRepository repository, IImageService imageService)
        {
            _repository = repository;
            _imageService = imageService;
        }

        public async Task<IEnumerable<BusinessDto>> GetAllAsync()
        {
            var businesses = await _repository.GetAllAsync();
            return businesses.Select(b => new BusinessDto
            {
                Id = b.Id,
                Name = b.Name,
                ImageUrl = b.ImageUrl,
                IsActive = b.IsActive,
                CreatedAt = b.CreatedAt,
                UserId = b.UserId
            });
        }

        public async Task<BusinessDto?> GetByIdAsync(Guid id)
        {
            var business = await _repository.GetByIdAsync(id);
            if (business == null) return null;

            return new BusinessDto
            {
                Id = business.Id,
                Name = business.Name,
                ImageUrl = business.ImageUrl,
                IsActive = business.IsActive,
                CreatedAt = business.CreatedAt,
                UserId = business.UserId
            };
        }

        public async Task<BusinessDto> CreateAsync(CreateBusinessDto createDto, Stream? imageStream, string? imageName)
        {
            string? imageUrl = null;
            if (imageStream != null && !string.IsNullOrEmpty(imageName))
            {
                imageUrl = await _imageService.UploadImageAsync(imageStream, imageName);
            }

            var newBusiness = new Business
            {
                Name = createDto.Name,
                UserId = createDto.UserId,
                ImageUrl = imageUrl,
                IsActive = true
            };

            var created = await _repository.AddAsync(newBusiness);

            return new BusinessDto
            {
                Id = created.Id,
                Name = created.Name,
                ImageUrl = created.ImageUrl,
                IsActive = created.IsActive,
                CreatedAt = created.CreatedAt,
                UserId = created.UserId
            };
        }

        public async Task<BusinessDto?> UpdateAsync(Guid id, UpdateBusinessDto updateDto, Stream? imageStream, string? imageName)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = updateDto.Name;
            existing.IsActive = updateDto.IsActive;

            if (imageStream != null && !string.IsNullOrEmpty(imageName))
            {
                var newImageUrl = await _imageService.UploadImageAsync(imageStream, imageName);
                if (!string.IsNullOrEmpty(newImageUrl))
                {
                    existing.ImageUrl = newImageUrl;
                }
            }

            await _repository.UpdateAsync(existing);

            return new BusinessDto
            {
                Id = existing.Id,
                Name = existing.Name,
                ImageUrl = existing.ImageUrl,
                IsActive = existing.IsActive,
                CreatedAt = existing.CreatedAt,
                UserId = existing.UserId
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
