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

        public string Email { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public string Mobilenumber { get; set; }

        public string Gender { get; set; }

        public int OrganizationId { get; set; }
    }
}
