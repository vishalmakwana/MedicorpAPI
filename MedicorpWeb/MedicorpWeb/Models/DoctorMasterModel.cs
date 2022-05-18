using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Models
{
    public class DoctorMasterModel
    {
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "StateId is required")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "CityId is required")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Mobilenumber is required")]
        public string Mobilenumber { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "OrganizationID is required")]
        public int OrganizationId { get; set; }
    }
}
