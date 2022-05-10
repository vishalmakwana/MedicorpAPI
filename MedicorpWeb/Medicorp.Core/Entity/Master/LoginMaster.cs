
using System.ComponentModel.DataAnnotations;


namespace Medicorp.Core.Entity
{
    public class LoginMaster
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

    
}
