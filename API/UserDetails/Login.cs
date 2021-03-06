
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Errors;
using API.Interfaces;
using API.Model;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace API.UserDetails
{
    public class Login
    {
        public class Query : IRequest<UserDTO>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, UserDTO>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;
            public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _signInManager = signInManager;
                _userManager = userManager;
            }

            public async Task<UserDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                IList<string> roles = await _signInManager.UserManager.GetRolesAsync(user);

                if (user == null)
                    throw new RestException(HttpStatusCode.Unauthorized);

                var result = await _signInManager
                    .CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    
                    string userPhoto = "";
                    try
                    {
                        userPhoto = user.Photos.Url;
                    }
                    catch (Exception)
                    {
                        userPhoto = "";
                    }

                    //string userPhoto = user.Photos.Url ?? "";
                    // TODO: generate token
                    return new UserDTO
                    {
                        DisplayName = user.DisplayName,
                        Token = _jwtGenerator.CreateToken(user, roles),
                        Username = user.UserName,
                        Email = user.Email,
                        Image = userPhoto,
                        UserRoles = roles
                    };
                }

                throw new RestException(HttpStatusCode.Unauthorized);
            }
        }
    }
}