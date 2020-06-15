using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Model;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.AppConfigList
{
    public class List
    {
        public class Query : IRequest<List<AppConfig>> { }

        public class Handler : IRequestHandler<Query, List<AppConfig>>
        {
            private readonly IMapper _mapper;

            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<List<AppConfig>> Handle(Query request, CancellationToken cancellationToken)
            {
                var list = await _context.AppConfig
                                    .ToListAsync();
                return list;
            }
        }
    }
}
