using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Model;
using AutoMapper;
using MediatR;

namespace API.Features._OrderMaster
{
    public class Details
    {
        public class Query : IRequest<OrderMasterDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, OrderMasterDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<OrderMasterDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var orderMaster = await _context.OrderMasters
                    .FindAsync(request.Id);

                if (orderMaster == null)
                    throw new RestException(HttpStatusCode.NotFound, new { OrderMaster = "Not found" });

                var toReturn = _mapper.Map <OrderMaster, OrderMasterDto>(orderMaster); 

                return toReturn;
            }
    }
}
}
