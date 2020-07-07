using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Model;
using AutoMapper;
using MediatR;

namespace API.Features._PagePhotos
{
    public class Details
    {
        public class Query : IRequest<PagePhotosDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagePhotosDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<PagePhotosDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var pagePhotos = await _context.PagePhotoss
                    .FindAsync(request.Id);

                if (pagePhotos == null)
                    throw new RestException(HttpStatusCode.NotFound, new { PagePhotos = "Not found" });

                var toReturn = _mapper.Map <PagePhotos, PagePhotosDto>(pagePhotos); 

                return toReturn;
            }
    }
}
}
