// namespace API.Features.Photos
// {
//     public class DeleteCatalogPhoto
//     {

//     }
// }

using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Photos
{
    public class DeleteCatalogPhoto
    {
        public class Command : IRequest
        {
            public Guid CatalogId { get; set; }
            public string PhotoId { get; set; }

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IPhotoAccessor _photoAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var catalog = await _context.Catalogs
                    .FindAsync(request.CatalogId);

                var CurrentUsername = _userAccessor.GetCurrentUsername();

                var photo = catalog.Photos.FirstOrDefault(x => x.Id == request.PhotoId);

                if (photo == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Photo = "Not found" });

                if (CurrentUsername.ToLower().ToString() == "admin" || catalog.Supplier.UserName == CurrentUsername)
                {
                    var result = _photoAccessor.DeletePhoto(photo.Id);

                    if (result == null)
                        throw new Exception("Problem deleting photo");
                    catalog.Photos.Remove(photo);

                    var success = await _context.SaveChangesAsync() > 0;

                    if (success) return Unit.Value;

                    throw new Exception("Problem saving changes");
                }
                else
                {
                    throw new Exception("Unauthorized access");
                }
            }
        }
    }
}