using System;
using System.ComponentModel.DataAnnotations;

namespace CP.Shared.Entities
{
    public class Business
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        
        // Navigation property
        public User? User { get; set; }
    }
}
