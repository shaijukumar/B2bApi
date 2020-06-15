
using API.Features.Photos;
using Microsoft.AspNetCore.Http;

namespace API.Interfaces
{
    public interface IPhotoAccessor
    {
        PhotoUploadResult AddPhoto(IFormFile file);
        string DeletePhoto(string publicId);
    }
}