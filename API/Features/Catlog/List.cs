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

namespace API.Features.Catlog
{
    public class List
    {
        public class Query : IRequest<List<CatlogListDto>> { }

        public class Handler : IRequestHandler<Query, List<CatlogListDto>>
        {
            private readonly IMapper _mapper;

            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<AppUser> _userManager;
            

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor, UserManager<AppUser> userManager )
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
                _userManager = userManager;
            }

            public async Task<List<CatlogListDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var currentUser = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

                _context.ChangeTracker.LazyLoadingEnabled = false;
                var catalogs = await _context.Catalogs
                    .Include(c => c.Photos)   
                    .Where( x => x.Supplier == currentUser )
                    .ToListAsync();
                //return catalogs;
                return _mapper.Map<List<Catalog>, List<CatlogListDto>>(catalogs);
               
                //var CurrentUsername = _userAccessor.GetCurrentUsername();

                //var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());
                //var roles = await _userManager.GetRolesAsync(user);
                //var r = _userManager.IsInRoleAsync(user, "AppAdmin");


                //if (CurrentUsername.ToLower().ToString() == "admin")
                //{

                //    var catalogs = await _context.Catalogs
                //    .ToListAsync();
                //    return _mapper.Map<List<Catalog>, List<CatlogDto>>(catalogs);
                //}
                //else
                //{
                //    var supplier = await _context.Users.SingleOrDefaultAsync(x =>
                //     x.UserName == _userAccessor.GetCurrentUsername());

                //    var catalogs = await _context.Catalogs
                //        .Where(c => c.Supplier.UserName == supplier.UserName)
                //        .ToListAsync();

                //    return _mapper.Map<List<Catalog>, List<CatlogDto>>(catalogs);
                //}
            }
        }
    }
}