using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.UserDetails
{
    public class UserDTO
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        //public string UserRoles { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
