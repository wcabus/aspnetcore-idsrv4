using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprotify.Web.Controllers
{
    public class LogoutController : Controller
    {
        public async Task Logout()
        {
            // sign out of the app
            await HttpContext.Authentication.SignOutAsync("Cookies");
            // sign out of the IDP
            await HttpContext.Authentication.SignOutAsync("oidc");
        }
    }
}
