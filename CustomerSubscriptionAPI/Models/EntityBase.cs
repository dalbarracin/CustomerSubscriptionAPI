using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerSubscriptionAPI.Models
{
    public abstract class EntityBase
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}
