using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Features.Photos;
using API.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PagePhotosController : BaseController
    {
        [HttpPost("{id}")]
        public async Task<ActionResult<PagePhoto>> Add(Guid id, [FromForm] AddPagePhoto.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

    }
}
