using System;

namespace Drivers_Management.Domain.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}