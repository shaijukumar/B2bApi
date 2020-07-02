using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Model;
using AutoMapper;
using MediatR;

namespace API.Features._OrderAttachments
{
    public class Details
    {
        public class Query : IRequest<OrderAttachmentsDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, OrderAttachmentsDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<OrderAttachmentsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var orderAttachments = await _context.OrderAttachmentss
                    .FindAsync(request.Id);

                if (orderAttachments == null)
                    throw new RestException(HttpStatusCode.NotFound, new { OrderAttachments = "Not found" });

                var toReturn = _mapper.Map <OrderAttachments, OrderAttachmentsDto>(orderAttachments); 

                return toReturn;
            }
    }
}
}
