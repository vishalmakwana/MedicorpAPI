
using System.ComponentModel.DataAnnotations;


namespace Medicorp.Core.Entity.Master
{
    public class SpecilityMaster
    {
        public int SpecilityId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        
        public bool IsActive { get; set; }

        public int OrganizationId { get; set; }

        public string InsertdBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }

    public class SpecilityMasterFilter
    {
        public int SpecilityId { get; set; }
        
        public string Title { get; set; }

        public bool IsActive { get; set; }
        
        public int OrganizationId { get; set; }
    }
}
