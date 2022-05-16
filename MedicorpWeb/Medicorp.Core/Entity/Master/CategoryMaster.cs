using System.ComponentModel.DataAnnotations;

namespace Medicorp.Core.Entity.Master
{
    public class CategoryMaster
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

        public int OrganizationId { get; set; }

        public string InsertdBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }

    public class CategoryMasterFilter
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

        public int OrganizationId { get; set; }

    }
}
