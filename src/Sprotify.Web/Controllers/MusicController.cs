using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Sprotify.Web.Models.Music;

namespace Sprotify.Web.Controllers
{
    public class MusicController : Controller
    {
        [Route("search")]
        public IActionResult Search()
        {
            return View();
        }

        [Route("browse")]
        public IActionResult Browse()
        {
            return View();
        }

        [Route("your-music")]
        public IActionResult YourMusic()
        {
            var data = new[] 
            {
                new PlaylistInfo
                {
                    Title = "Guardians of the Galaxy Vol. 2",
                    Owner = new OwnerInfo
                    {
                        UserName = "wcabus",
                        FullName = "Wesley Cabus"
                    }
                },
                new PlaylistInfo
                {
                    Title = "Distant Worlds - Music from Final Fantasy",
                    Owner = new OwnerInfo
                    {
                        UserName = "wcabus",
                        FullName = "Wesley Cabus"
                    }
                },
                new PlaylistInfo
                {
                    Title = "Theme Awesome",
                    Owner = new OwnerInfo
                    {
                        UserName = "jwilms",
                        FullName = "Jan Wilms"
                    }
                }
            };
            return View(data);
        }
    }
}
