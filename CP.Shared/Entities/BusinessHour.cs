using System;
using System.ComponentModel.DataAnnotations;

namespace CP.Shared.Entities
{
    public class BusinessHour
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BusinessId { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        
        // This property is inferred as per the request, though usually could be explicit. 
        // We can just set it based on whether StartTime != EndTime or similar logic if needed, 
        // but for now, we'll keep it as a simple property.
        public bool IsOpen { get; set; } = true; 

        // Navigation property
        public Business? Business { get; set; }
    }
}
