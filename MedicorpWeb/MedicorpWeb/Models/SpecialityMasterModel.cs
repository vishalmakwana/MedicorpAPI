using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Models
{
    public class SpecialityMasterModel
    {
        public int SpecialityId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public bool IsActive { get; set; }
        [Required(ErrorMessage = "OrganizationId is required")]
        public int OrganizationId { get; set; }
    }
}