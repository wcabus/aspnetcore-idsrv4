using Microsoft.AspNetCore.Mvc;
using Sprotify.Web.Models.User;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sprotify.Web.ViewComponents
{
    public class LoginPanel : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // get name
            string name = string.Empty;

            // option one: access the claims
            var ci = User.Identity as ClaimsIdentity;
            name = $"{ci.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value} {ci.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value}";
            
            return View(new LoginPanelModel
            {
                // option one: access the claims
                Name = name
            });
        }
    }
}
