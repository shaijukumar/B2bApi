//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace API.UserDetails
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Model;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace API.UserDetails
{
    public class UserList
    {
        public class Query : IRequest<List<AppUser>> { }

        public class Handler : IRequestHandler<Query, List<AppUser>>
        {
            private readonly IMapper _mapper;

            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor, RoleManager<IdentityRole> roleManager)
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
                _roleManager = roleManager;
            }

            public async Task<List<AppUser>> Handle(Query request, CancellationToken cancellationToken)
            {
                var appUsers = await _context.Users.ToListAsync();
               // var r  = _roleManager.FindByNameAsync("Administrator");
                


               // appUsers[0].isi


                return appUsers;
            }

            
        }
    }
}