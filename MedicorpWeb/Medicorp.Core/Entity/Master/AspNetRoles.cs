using System.ComponentModel.DataAnnotations;


namespace Medicorp.Core.Entity.Master
{
    public class AspNetRoles
    {
        public string Id { get; set; }

       public string ConcurrencyStamp { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string NormalizedName { get; set; }
                
    }

    public class AspNetRolesFilter
    {
        public string Id { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
