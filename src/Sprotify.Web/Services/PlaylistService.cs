using Sprotify.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sprotify.Web.Services
{
    public class PlaylistService : ApiServiceBase
    {
        public PlaylistService(HttpClient client, string baseUri) : base(client, baseUri) { }

        public async Task<IEnumerable<Playlist>> GetPlaylists()
        {
            return await Get<IEnumerable<Playlist>>("playlists").ConfigureAwait(false);
        }

        public async Task<PlaylistWithSongs> GetPlaylist(Guid id)
        {
            return await Get<PlaylistWithSongs>($"playlists/{id}?expand=true").ConfigureAwait(false);
        }
    }
}
