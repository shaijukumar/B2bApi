// namespace API.Features.Catlog
// {
//     public class Details
//     {

//     }
// }

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Model;
using AutoMapper;
using MediatR;

namespace API.Features.Catlog
{
    public class Details
    { 
        public class Query : IRequest<CatlogDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, CatlogDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<CatlogDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var catlog = await _context.Catalogs
                    .FindAsync(request.Id);

                if (catlog == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "Not found" });

                var toReturn = _mapper.Map<Catalog, CatlogDto>(catlog); 

                return toReturn;
            }
        }
    }
}