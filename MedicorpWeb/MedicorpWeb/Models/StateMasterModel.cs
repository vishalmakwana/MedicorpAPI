using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Models
{
    public class StateMasterModel
    {
        public int StateId { get; set; }
        [Required(ErrorMessage = "State Name is required")]
        public string StateName { get; set; }
        public bool IsActive { get; set; }
    }
}
