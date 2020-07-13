using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Interfaces;
using MediatR;

namespace API.Features._StockManagemnt
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
                var stockManagemnt = await _context.StockManagemnts
                    .FindAsync(request.Id);
                if (stockManagemnt == null)
                    throw new RestException(HttpStatusCode.NotFound, new { StockManagemnt = "Not found" });

                var CurrentUsername = _userAccessor.GetCurrentUsername();

                _context.Remove(stockManagemnt);
				var success = await _context.SaveChangesAsync() > 0;
				if (success) return Unit.Value;

                throw new Exception("Problem saving changes");

            }

        }

    }
}
