using Microsoft.AspNetCore.Mvc;
using Sprotify.Web.Models.User;
using System;

namespace Sprotify.Web.Controllers
{
    public class UserController : Controller
    {
        [Route("user-info/{owner:guid}")]
        public IActionResult Info([FromRoute]Guid owner)
        {
            return View(new UserInfo { FullName = "Wesley Cabus", Id = owner });
        }
    }
}
