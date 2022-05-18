using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Models
{
    public class CityMasterModel
    {
        public int CityId { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "State Id is required")]
        public int StateId { get; set; }
        public bool IsActive { get; set; }
    }
}
