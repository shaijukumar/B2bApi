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

namespace API.Features._StockCat
{
    public class List
    {
        public class Query : IRequest<List<StockCatDto>> { }

        public class Handler : IRequestHandler<Query, List<StockCatDto>>
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

            public async Task<List<StockCatDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var stockCat = await _context.StockCats
                    .ToListAsync();
					
                return _mapper.Map<List<StockCat>, List<StockCatDto>>(stockCat);
                
            }
        }
    }
}
