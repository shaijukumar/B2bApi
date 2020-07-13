using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Model;
using AutoMapper;
using MediatR;

namespace API.Features._StockCat
{
    public class Details
    {
        public class Query : IRequest<StockCatDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, StockCatDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<StockCatDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var stockCat = await _context.StockCats
                    .FindAsync(request.Id);

                if (stockCat == null)
                    throw new RestException(HttpStatusCode.NotFound, new { StockCat = "Not found" });

                var toReturn = _mapper.Map <StockCat, StockCatDto>(stockCat); 

                return toReturn;
            }
    }
}
}
