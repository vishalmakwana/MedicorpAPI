using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Model
{
    public class MemberCredentialModel
    {
        [Required, MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
