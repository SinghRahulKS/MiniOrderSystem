using System.ComponentModel.DataAnnotations;

namespace UserService.Model.Request
{
    public class AddressRequestModel
    {
        [Required]
        [StringLength(200)]
        public string Street { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string State { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string PostalCode { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Country { get; set; } = null!;

        public bool IsDefault { get; set; } = false; // Optional: default shipping address
    }
}
