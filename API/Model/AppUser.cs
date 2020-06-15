using Microsoft.AspNetCore.Identity;

namespace API.Model
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public virtual UserPhoto Photos { get; set; }
    }
}