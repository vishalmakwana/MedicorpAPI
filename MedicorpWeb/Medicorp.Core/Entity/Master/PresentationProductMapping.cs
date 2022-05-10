using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.Core.Entity.Master
{
    public class PresentationProductMapping
    {
        public int PresentationProductMappingId { get; set; }

        public int OrganizationId { get; set; }

        public int UserPresentationMasterId { get; set; }

        public int ProductId { get; set; }

        public string InsertdBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }

    public class PresentationProductMappingFilter
    {
        public int? PresentationProductMappingId { get; set; }

        public int OrganizationId { get; set; }

        public int UserPresentationMasterId { get; set; }

        public int ProductId { get; set; }
    }
}
