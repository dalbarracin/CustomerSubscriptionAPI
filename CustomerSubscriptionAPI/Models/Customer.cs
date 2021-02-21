using System.ComponentModel.DataAnnotations;

namespace CustomerSubscriptionAPI.Models
{
    public class Customer : EntityBase
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; }
    }
}
