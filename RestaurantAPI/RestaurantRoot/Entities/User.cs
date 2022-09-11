using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.RestaurantRoot.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public DateTime ? DateOfBirth { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
