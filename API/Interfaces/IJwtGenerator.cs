using API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IJwtGenerator
    {
         string CreateToken(AppUser user, IList<string> roles);
    }
}