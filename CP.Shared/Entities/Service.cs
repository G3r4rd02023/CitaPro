using System;
using System.ComponentModel.DataAnnotations;

namespace CP.Shared.Entities
{
    public class Service
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BusinessId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int IntDurationMinutes { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation property
        public Business? Business { get; set; }
    }
}
