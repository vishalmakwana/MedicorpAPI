namespace MedicorpWeb.Models
{
    public class OrganizationMasterModel
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }

        public string OrganizationPrefix { get; set; }
        public bool IsActive { get; set; }
    }
}
