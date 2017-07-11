using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sprotify.API.Models;
using Sprotify.API.Services;
using System;
using System.Collections.Generic;

namespace Sprotify.API.Controllers
{
    [Route("api/playlists")]
    public class PlaylistsController : Controller
    {
        private readonly ISprotifyRepository _sprotifyRepository;

        public PlaylistsController(ISprotifyRepository sprotifyRepository)
        {
            _sprotifyRepository = sprotifyRepository;
        }
        
        [HttpGet]
        public IActionResult GetPlaylists()
        {
            var playlists = _sprotifyRepository.GetPlaylists();
            return Ok(Mapper.Map<IEnumerable<Playlist>>(playlists));
        }

        [HttpGet("~/api/users/{userId:guid}/playlists")]
        public IActionResult GetPlaylistsForUser(Guid userId)
        {
            if (!_sprotifyRepository.UserExists(userId))
            {
                return NotFound();
            }

            var playlists = _sprotifyRepository.GetPlaylistsFromUser(userId);
            return Ok(Mapper.Map<IEnumerable<Playlist>>(playlists));
        }

        [HttpGet("{playlistId}", Name = "GetPlaylist")]
        public IActionResult GetPlaylist(Guid playlistId, [FromQuery] bool expand)
        {
            var playlist = _sprotifyRepository.GetPlaylist(playlistId, expand);

            if (playlist == null)
            {
                return NotFound();
            }

            if (expand)
            {
                var playlistWithSongsToReturn = Mapper.Map<PlaylistWithSongs>(playlist);
                return Ok(playlistWithSongsToReturn);
            }

            return Ok(Mapper.Map<Playlist>(playlist));
        }

        [HttpPost]
        public IActionResult CreatePlaylist([FromBody] PlaylistForCreation playlistForCreation)
        {
            if (playlistForCreation == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var mappedPlaylist = Mapper.Map<Entities.Playlist>(playlistForCreation);

            _sprotifyRepository.CreatePlaylist(mappedPlaylist);

            if (!_sprotifyRepository.Save())
            {
                throw new Exception("Creating the playlist failed.");
            }

            var createdPlaylistToReturn = Mapper.Map<Models.Playlist>(mappedPlaylist);

            return CreatedAtRoute("GetPlaylist",
                new
                {
                    playlistId = mappedPlaylist.Id
                },
                createdPlaylistToReturn);
        }
    }
}
