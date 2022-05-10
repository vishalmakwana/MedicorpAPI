
namespace Medicorp.Core.Entity.Master
{
    public class DoctorSpecilityMapping
    {
        public int DoctorSpecilityMappingId { get; set; }

        public int OrganizationId { get; set; }

        public int DoctorId { get; set; }

        public int SpecilityId { get; set; }

        public string InsertdBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }

    public class DoctorSpecilityMappingFilter
    {
        public int DoctorSpecilityMappingId { get; set; }

        public int DoctorId { get; set; }

        public int OrganizationId { get; set; }

        public int SpecilityId { get; set; }
    }
}
