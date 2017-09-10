using Sprotify.Web.Models.Api;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sprotify.Web.Services
{
    public class StatsService : ApiServiceBase
    {
        public StatsService(HttpClient client, string baseUri) : base(client, baseUri)
        {
        }

        public async Task<int> GetPlaylistCount()
        {
            var list = await Get<IEnumerable<Playlist>>("playlists").ConfigureAwait(false);
            return list.Count();
        }
    }
}
