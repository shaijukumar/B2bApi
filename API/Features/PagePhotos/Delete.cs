using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Interfaces;
using MediatR;

namespace API.Features._PagePhotos
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
                var pagePhotos = await _context.PagePhotoss
                    .FindAsync(request.Id);
                if (pagePhotos == null)
                    throw new RestException(HttpStatusCode.NotFound, new { PagePhotos = "Not found" });

                var CurrentUsername = _userAccessor.GetCurrentUsername();

                _context.Remove(pagePhotos);
				var success = await _context.SaveChangesAsync() > 0;
				if (success) return Unit.Value;

                throw new Exception("Problem saving changes");

            }

        }

    }
}
