using System;

namespace CP.Shared.DTOs
{
    public class BusinessDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
    }

    public class CreateBusinessDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        // Image will be handled via Multipart/Form-Data, not directly in JSON DTO usually, 
        // but for Service method signatures we can pass Stream separately.
    }

    public class UpdateBusinessDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
