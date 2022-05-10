using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Models
{
    public class UserMasterModel
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int OrganizationId { get; set; }

        [Required(ErrorMessage = "Organization Name is required")]
        public int OrganizationName { get; set; }
    }
}
