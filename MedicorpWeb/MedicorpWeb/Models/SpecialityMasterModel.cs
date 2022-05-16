using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Models
{
    public class SpecialityMasterModel
    {
<<<<<<< HEAD
        public int SpecialityId { get; set; }
=======
        public int SpecilityId { get; set; }
>>>>>>> efbde2b0d63a6da6132242e2ed57eea04d9abe47

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int OrganizationId { get; set; }
    }
}