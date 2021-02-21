using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CustomerSubscriptionAPI.Models
{
    public class Product : EntityBase
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
