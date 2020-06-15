using System.Collections.Generic;
using System.Threading.Tasks;
using API.Features.AppConfigList;
using API.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AppConfigController : BaseController
    {[HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<AppConfig>>> List()
        {
            return await Mediator.Send(new List.Query());
        }
    }
}