//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace API.Features.Pages
//{
//    public class AddPagePhoto
//    {
//    }
//}

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
    public class AddPagePhoto
    {
        public class Command : IRequest<PagePhoto>
        {
            public IFormFile File { get; set; }

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, PagePhoto>
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

            public async Task<PagePhoto> Handle(Command request, CancellationToken cancellationToken)
            {
                var photoUploadResult = _photoAccessor.AddPhoto(request.File);

                var page = await _context.Pages
                    .FindAsync(request.Id);


                var photo = new PagePhoto
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId
                };

                page.Photos.Add(photo);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return photo;

                throw new Exception("Problem saving changes");

            }

        }

    }
}
