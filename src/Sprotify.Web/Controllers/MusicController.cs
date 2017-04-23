using System;
using Microsoft.AspNetCore.Mvc;
using Sprotify.Web.Models.Music;
using Sprotify.Web.Services;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;

namespace Sprotify.Web.Controllers
{
    public class MusicController : Controller
    {
        private readonly PlaylistService _playlistService;

        public MusicController(PlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

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
        public async Task<IActionResult> YourMusic()
        {
            var playlists = await _playlistService.GetPlaylists();
            return View(Mapper.Map<IEnumerable<PlaylistInYourMusic>>(playlists));
        }

        [Route("playlist/{id:guid}")]
        public async Task<IActionResult> Playlist([FromRoute]Guid id)
        {
            var playlist = await _playlistService.GetPlaylist(id);
            return View(Mapper.Map<PlaylistDetails>(playlist));
        }
    }
}
