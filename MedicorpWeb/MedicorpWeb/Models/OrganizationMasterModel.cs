using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Models
{
    public class OrganizationMasterModel
    {
        public int OrganizationId { get; set; }

        [Required(ErrorMessage = "Organization Name is required")]
        public string OrganizationName { get; set; }

        public string OrganizationPrefix { get; set; }
        public bool IsActive { get; set; }
    }
}
