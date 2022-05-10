using System.ComponentModel.DataAnnotations;

namespace MedicorpWeb.Model
{
    public class MemberRegisterModel
    {
        [Required, MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(256)]
        public string MobileNo { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(256)]
        public string FirstName { get; set; }
        [Required, MaxLength(256)]
        public string LastName { get; set; }

    }
}