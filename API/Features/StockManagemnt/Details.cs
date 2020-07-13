using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Model;
using AutoMapper;
using MediatR;

namespace API.Features._StockManagemnt
{
    public class Details
    {
        public class Query : IRequest<StockManagemntDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, StockManagemntDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<StockManagemntDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var stockManagemnt = await _context.StockManagemnts
                    .FindAsync(request.Id);

                if (stockManagemnt == null)
                    throw new RestException(HttpStatusCode.NotFound, new { StockManagemnt = "Not found" });

                var toReturn = _mapper.Map <StockManagemnt, StockManagemntDto>(stockManagemnt); 

                return toReturn;
            }
    }
}
}
