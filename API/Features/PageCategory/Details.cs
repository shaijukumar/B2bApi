using System;
using System.Threading;
using MediatR;
using System.Threading.Tasks;
using API.Data;
using API.Model;
using AutoMapper;
using API.Errors;
using System.Net;

namespace API.Features.PageCategory
{
    public class Details
    {
        public class Query : IRequest<PageItemCategoryDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, PageItemCategoryDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<PageItemCategoryDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var pageItemCategory  = await _context.PageItemCategorys                    
                    .FindAsync(request.Id);

                if (pageItemCategory == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "Not found" });

               return _mapper.Map<PageItemCategory, PageItemCategoryDto>(pageItemCategory);

               
            }
        }
            
    }
}
