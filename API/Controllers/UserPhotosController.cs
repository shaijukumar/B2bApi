using System.Threading.Tasks;
using API.Features.Photos;
using API.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
     public class UserPhotosController : BaseController
    {
         [HttpPost]
        public async Task<ActionResult<UserPhoto>> Add([FromForm]AddUserPhoto.Command command)
        {
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