using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Interfaces;
using MediatR;

namespace API.Features._OrderAttachments
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var orderAttachments = await _context.OrderAttachmentss
                    .FindAsync(request.Id);
                if (orderAttachments == null)
                    throw new RestException(HttpStatusCode.NotFound, new { OrderAttachments = "Not found" });

                var CurrentUsername = _userAccessor.GetCurrentUsername();

                _context.Remove(orderAttachments);
				var success = await _context.SaveChangesAsync() > 0;
				if (success) return Unit.Value;

                throw new Exception("Problem saving changes");

            }

        }

    }
}
