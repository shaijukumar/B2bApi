using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Model;
using AutoMapper;
using MediatR;


namespace Application.Pages
{
    public class Details
    {
        public class Query : IRequest<PageDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, PageDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                 _mapper = mapper;
                _context = context;
            }

            public async Task<PageDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var page  = await _context.Pages                    
                    .FindAsync(request.Id);
                
                if (page == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "Not found" });

                var pageToReturn = _mapper.Map<Page, PageDto>(page);

                return pageToReturn;
            }
        }
            
    }
}