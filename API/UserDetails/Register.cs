//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Interfaces;
using API.Model;
using API.Validators;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.UserDetails
{
    public class Register
    {
        public class Command : IRequest<User>
        {
            public string DisplayName { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }            
            public string Password { get; set; }
            public string UserType { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
                RuleFor(x => x.UserType).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(DataContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
                _context = context;
            }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                var role = _context.Roles.SingleOrDefault(x => x.Name == request.UserType);
                if(role == null)
                {
                    throw new Exception("invalid user role(UserType)");
                }

                if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });

                if (await _context.Users.Where(x => x.UserName == request.Username).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });

                var user = new AppUser
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    UserName = request.Username
                    
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    try
                    {
                        await _userManager.AddToRoleAsync(user, request.UserType);
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("Problem creating user role");
                    }

                    IList<string> roles = await _userManager.GetRolesAsync(user);

                    string userPhoto = "";
                    try
                    {
                        userPhoto = user.Photos.Url;
                    }
                    catch (Exception)
                    {
                        userPhoto = "";
                    }
                    return new User
                    {
                        DisplayName = user.DisplayName,
                        Token = _jwtGenerator.CreateToken(user, roles),
                        Username = user.UserName,
                        Image = userPhoto
                    };
                }

                throw new Exception("Problem creating user");
            }
        }
    }
}