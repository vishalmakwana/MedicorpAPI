using System.ComponentModel.DataAnnotations;


namespace Medicorp.Core.Entity.Master
{
    public class ImageMaster
    {
        public int ImageId { get; set; }

        [Required(ErrorMessage = "URL is required")]
        public string URL { get; set; }

        public bool IsDelete { get; set; }

        public bool IsActive { get; set; }

        public int OrganizationId { get; set; }

        public int ProductId { get; set; }

        public string InsertdBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }

    public class ImageMasterFilter
    {
        public int ImageId { get; set; }

        public string URL { get; set; }

        public bool IsActive { get; set; }

        public int OrganizationId { get; set; }

        public int ProductId { get; set; }
    }
}
