using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Models
{
    public class CategoryMasterModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "OrganizationId is required")]
        public int OrganizationId { get; set; }
    }
}
