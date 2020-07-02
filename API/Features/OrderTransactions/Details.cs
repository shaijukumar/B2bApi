using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Model;
using AutoMapper;
using MediatR;

namespace API.Features._OrderTransactions
{
    public class Details
    {
        public class Query : IRequest<OrderTransactionsDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, OrderTransactionsDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<OrderTransactionsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var orderTransactions = await _context.OrderTransactionss
                    .FindAsync(request.Id);

                if (orderTransactions == null)
                    throw new RestException(HttpStatusCode.NotFound, new { OrderTransactions = "Not found" });

                var toReturn = _mapper.Map <OrderTransactions, OrderTransactionsDto>(orderTransactions); 

                return toReturn;
            }
    }
}
}
