//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace API.Security
//{
//    public class IsAdmin
//    {
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;


namespace API.Security
{
    public class IsAdmin : IAuthorizationRequirement
    {
    }

    public class IsAdminHandler : AuthorizationHandler<IsAdmin>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly UserManager<AppUser> _userManager;
        public IsAdminHandler(IHttpContextAccessor httpContextAccessor, DataContext context, IUserAccessor userAccessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _userAccessor = userAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdmin requirement)
        {
            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }


            //var v = _httpContextAccessor.HttpContext.User.IsInRole("AppAdmin");

            ////var role1 = _httpContextAccessor.HttpContext.User?.Claims?
            ////    .SingleOrDefault(x => x.Type == ClaimTypes.Role);


            //string GroupName = "AppAdmin";
            //var role = _context.Roles.SingleOrDefault(x => x.Name == GroupName);

            //var userId = _httpContextAccessor.HttpContext.User?.Claims?
            //    .SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            //var role1 = _httpContextAccessor.HttpContext.User?.Claims?
            //    .SingleOrDefault(x => x.Type == ClaimTypes.Role )?.Value;

            //var user = await _userManager.FindBy .FindByNameAsync(_userAccessor.GetCurrentUsername());
            //var roles = await _userManager.GetRolesAsync(user);
            //var r = _userManager.IsInRoleAsync(user, "AppAdmin");

            

            // var s = string.Empty;

            // _httpContextAccessor.HttpContext.User?.Claims.ToList().ForEach(x =>
            //{
            //    s = x.Value;
            //    s = x.Value;
            //});




            //var userId = _httpContextAccessor.HttpContext.User?.Claims?.
            //            SingleOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

           // var userRole = _context.UserRoles.SingleOrDefault(x => (x.UserId == userId && x.RoleId == role.Id));

            //if (userRole != null)
            //{
            //    context.Succeed(requirement);
            //}

            return Task.CompletedTask;

        }
    }
}