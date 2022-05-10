using System.ComponentModel.DataAnnotations;

namespace Medicorp.Core.Entity
{
    public class UserMaster
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string Lastname { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }
        public bool IsActive { get; set; }

        public int OrganizationId { get; set; }

        
    }

    public class UserMasterFilter
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool IsActive { get; set; }
        public int OrganizationId { get; set; }

        public string OrganizationName { get; set; }
    }
}
