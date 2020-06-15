using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Photos
{
    public class AddCatalogPhoto
    {
        public class Command : IRequest<CatalogPhoto>
        {
            public IFormFile File { get; set; }

            public Guid CatalogId { get; set; }
        }

        public class Handler : IRequestHandler<Command, CatalogPhoto>
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

            public async Task<CatalogPhoto> Handle(Command request, CancellationToken cancellationToken)
            {
                var photoUploadResult = _photoAccessor.AddPhoto(request.File);

                var catalog = await _context.Catalogs
                //  .FindAsync(new Guid("ac47351e-401c-4714-8b8e-baedddb0cbef"));
                    .FindAsync(request.CatalogId);

                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetCurrentUsername());

                var photo = new CatalogPhoto
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId
                };

                if (!catalog.Photos.Any(x => x.IsMain))
                    photo.IsMain = true;

                catalog.Photos.Add(photo);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return photo;

                throw new Exception("Problem saving changes");

            }

        }

    }
}