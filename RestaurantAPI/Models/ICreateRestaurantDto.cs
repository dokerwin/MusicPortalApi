using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public interface ICreateRestaurantDto
    {
        string Category { get; set; }
        string City { get; set; }
        string ContactEmail { get; set; }
        string ContactPhone { get; set; }
        string Description { get; set; }
        bool HasDelivery { get; set; }

        [Required]
        [MaxLength(25)]
        string Name { get; set; }
        string PostalCode { get; set; }
        string Street { get; set; }
    }
}