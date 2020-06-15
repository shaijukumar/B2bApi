using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Interfaces;
using API.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace API.UserDetails
{
    public class CurrentUser
    {
        public class Query : IRequest<User> { }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserAccessor _userAccessor;
            public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenerator, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
                _userManager = userManager;
            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());
                var roles   = await _userManager.GetRolesAsync(user);

                return new User
                {
                    DisplayName = user.DisplayName,
                    Username = user.UserName,
                    Email = user.Email,
                    //Token = _jwtGenerator.CreateToken(user),                   
                    //Image = user.Photos.Url
                };
            }
        }
    }
}