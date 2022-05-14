using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.Core.Entity.Master
{
    public class ProductCategoryMapping
    {
        public int ProductCategoryMappingId { get; set; }

        public int OrganizationId { get; set; }

        public int CategoryId { get; set; }

        public int ProductId { get; set; }

        public string InsertdBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public Boolean IsActive { get; set; }

    }

    public class ProductCategoryMappingFilter
    {
        public int ProductCategoryMappingId { get; set; }

        public int OrganizationId { get; set; }

        public int CategoryId { get; set; }

        public int ProductId { get; set; }
    }
}
