using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.Core.Entity.Master
{
    public class UserPresentationMaster
    {
        public int UserPresentationMasterId { get; set; }

        public int OrganizationId { get; set; }

        public int DoctorId { get; set; }

        public int UserId { get; set; }

        public DateTime PresentationDate { get; set; }

        public string latitube { get; set; }
        
        public string longitude { get; set; }
        
        public string InsertdBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }

    public class UserPresentationMasterFilter
    {
        public int? UserPresentationMasterId { get; set; }

        public int OrganizationId { get; set; }

        public int DoctorId { get; set; }

        public int UserId { get; set; }

        public DateTime PresentationDate { get; set; }
    }
}
