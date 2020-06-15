using System;
using System.Threading.Tasks;
using API.Features.Photos;
using API.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CatalogPhotoController : BaseController
    {
        //[HttpPost]
        [HttpPost("{id}")]
        public async Task<ActionResult<CatalogPhoto>> Add(Guid id, [FromForm] AddCatalogPhoto.Command command)
        {
            command.CatalogId = id;
            return await Mediator.Send(command);
        }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Unit>> Delete(string id)
        // {
        //     return await Mediator.Send(new Delete.Command{Id = id});
        // }

        // [HttpPost("{id}/setmain")]
        // public async Task<ActionResult<Unit>> SetMain(string id)
        // {
        //     return await Mediator.Send(new SetMain.Command{Id = id});
        // }
    }
}