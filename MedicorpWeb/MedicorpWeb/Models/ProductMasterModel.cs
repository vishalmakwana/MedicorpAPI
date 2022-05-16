using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Models
{
    public class ProductMasterModel
    {

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "ProductDescription is required")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "MRP is required")]
        public string MRP { get; set; }
        public bool IsActive { get; set; }
        public int OrganizationId { get; set; }

        public int CategoryId { get; set; }
    }
}
