using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.Core.Entity.Master
{
    public class StateMaster
    {
        public int StateId { get; set; }
        [Required(ErrorMessage = "State Name is required")]
        public string StateName { get; set; }
        public bool IsActive { get; set; }
    }
}
