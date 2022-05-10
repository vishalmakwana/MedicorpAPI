
using System.ComponentModel.DataAnnotations;


namespace Medicorp.Core.Entity.Master
{
    public class ProductMaster
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "ShortDescription is required")]
        public string ShortDescription { get; set; }
       
        public string LongDescription { get; set; }

        public string MRP { get; set; }
        
        public bool IsActive { get; set; }

        public int OrganizationId { get; set; }

        public string InsertdBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }

    public class ProductMasterFilter
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string OrganizationName { get; set; }

        public string MRP { get; set; }
        public bool IsActive { get; set; }
        public int OrganizationId { get; set; }
    }
}
