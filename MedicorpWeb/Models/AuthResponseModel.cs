using System;

namespace MedicorpWeb.Model
{
    public class AuthResponseModel 
    {
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
