using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerSubscriptionAPI.Models
{
    public class Subscription
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
    }
}
