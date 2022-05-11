using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Models
{
    public class ProductMasterModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "ShortDescription is required")]
        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string OrganizationName { get; set; }

        [Required(ErrorMessage = "MRP is required")]
        public string MRP { get; set; }
        public bool IsActive { get; set; }
        public int OrganizationId { get; set; }
    }
}
