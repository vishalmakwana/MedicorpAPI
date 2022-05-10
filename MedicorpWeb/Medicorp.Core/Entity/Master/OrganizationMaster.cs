using System.ComponentModel.DataAnnotations;


namespace Medicorp.Core.Entity
{
    public class OrganizationMaster
    {
        public int OrganizationId { get; set; }

        [Required(ErrorMessage = "Organization Name is required")]
        public string OrganizationName { get; set; }

       public string OrganizationPrefix { get; set; }
       
        public bool IsActive { get; set; }

        public string InsertBy { get; set; }
        
        public string UpdateBy { get; set; }
        
        public DateTime InsertedDate { get; set; }
        
        public DateTime UpdateDate { get; set; }
    }

    public class OrganizationMasterFilter
    {
        public int? OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationPrefix { get; set; }
        public bool IsActive { get; set; }

    }
}
