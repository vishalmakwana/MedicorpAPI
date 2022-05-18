using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Models
{
    public class ProductMasterModel
    {

        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "ProductDescription is required")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "MRP is required")]
        public string MRP { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "OrganizationID is required")]
        public int OrganizationId { get; set; }

        [Required(ErrorMessage = "CategoryID is required")]
        public int CategoryId { get; set; }
    }
}
