using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Models
{
    public class RolesModel
    {
        public string Id { get; set; }
        public string ConcurrencyStamp { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
