using Microsoft.AspNetCore.Mvc;
using Sprotify.Web.Models.User;
using System.Threading.Tasks;

namespace Sprotify.Web.ViewComponents
{
    public class LoginPanel : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("Login");
            }

            return View(new LoginPanelModel
            {
                Name = User.Identity.Name
            });
        }
    }
}
