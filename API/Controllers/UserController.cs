using System.Threading.Tasks;
using API.Model;
using API.UserDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System;

namespace API.Controllers
{

    public class UserController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(Register.Command command)
        {
            return await Mediator.Send(command);
        }

        //[HttpGet]
        //public async Task<ActionResult<User>> CurrentUser()
        //{
        //    return await Mediator.Send(new CurrentUser.Query());
        //}

        //[HttpGet]       
        //public async Task<ActionResult<AppUser>> GetAllUsers(Login.l query)
        //{
        //    return await Mediator.Send(new List.Query());
        //    //return await Mediator.Send(query);
        //}

        //[HttpGet]    
        //public async Task<ActionResult<List<AppUser>>> GetAllUsers()
        //{
        //    return await Mediator.Send(new UserList.Query());
        //}


        //[HttpGet]
        //public string GetAllUsers()
        //{
        //    return "test";
        //   // return await Mediator.Send(new UserList.Query());
        //}

        [HttpGet]
        //[AllowAnonymous]
        [Authorize(Policy = "IsAppAdmin")]
        public async Task<ActionResult<List<AppUser>>> Get()
        {
            return await Mediator.Send(new UserList.Query());
        }


        //[HttpGet]
        //[Authorize(Policy = "IsAppAdmin")]
        //public string Get()
        //{
        //    return "Test - " + DateTime.Now.ToString();
        //}

    }
}