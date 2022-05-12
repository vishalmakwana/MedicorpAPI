using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.Core.Entity.Master
{
    public class CityMaster
    {
        public int CityId { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "State Id is required")]
        public int StateId { get; set; }
        public bool IsActive { get; set; }
    }
}
