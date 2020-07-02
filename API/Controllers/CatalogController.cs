using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Features.Catlog;
using API.Features.Photos;
using API.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CatalogController : BaseController
    {

        [HttpGet]        
        public async Task<ActionResult<List<CatlogListDto>>> List()
        {
            return await Mediator.Send(new List.Query());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CatlogDto>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }


        [HttpPost]
        public async Task<ActionResult<CatlogDto>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CatlogDto>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Unit>> DeleteCatalogPhoto(DeleteCatalogPhoto.Command command)
        {
            return await Mediator.Send(command);
        }

        //// GET api/Catalog/ResellerCatalogList/5
        //[HttpGet("ResellerCatalogList/{id}")]
        //public async Task<ActionResult<List<CatlogListDto>>> ResellerCatalogList(string id, ResellerCatalogList.Command command)
        //{
        //    //command.SupplierPhone = id;
        //    //return await Mediator.Send(command);

        //}

        // GET api/Catalog/ResellerCatalogList/5
        [HttpGet("ResellerCatalogList/{id}")]
        public async Task<ActionResult<List<CatlogListDto>>> ResellerCatalogList(string id)
        {
            return await Mediator.Send(new ResellerCatalogList.Query { SupplierPhone = id });

            //ResellerCatalogList.Command command = 
            //    new Features.Catlog.ResellerCatalogList.Command();
            //command.SupplierPhone = id;
            //return await Mediator.Send(command);

            ////return "test 123";

            //
            //

        }



    }
}