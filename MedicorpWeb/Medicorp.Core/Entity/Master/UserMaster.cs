using System.ComponentModel.DataAnnotations;

namespace Medicorp.Core.Entity
{
    public class UserMaster
    {
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

    public class UserMasterFilter
    {
        public int? UserId { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public int OrganizationId { get; set; }
    }
}
