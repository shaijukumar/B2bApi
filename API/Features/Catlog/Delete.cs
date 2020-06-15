using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Interfaces;
using MediatR;

namespace API.Features.Catlog
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
                var catalog = await _context.Catalogs
                    .FindAsync(request.Id);
                if (catalog == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Catalog = "Not found" });

                var CurrentUsername = _userAccessor.GetCurrentUsername();

                if (CurrentUsername.ToLower().ToString() == "admin" || catalog.Supplier.UserName == CurrentUsername)
                {
                    _context.Remove(catalog);
                    var success = await _context.SaveChangesAsync() > 0;
                    if (success) return Unit.Value;
                }
                else
                {
                    throw new Exception("Unauthorized access");
                }


                throw new Exception("Problem saving changes");

            }

        }

    }
}