using Sprotify.API.Entities;
using System;
using System.Collections.Generic;
using Sprotify.API.Services.Models;

namespace Sprotify.API.Services
{
    public interface ISprotifyRepository
    {
        bool PlaylistExists(Guid playlistId);
        IEnumerable<Playlist> GetPlaylists();
        PlaylistWithSongs GetPlaylist(Guid playlistId, bool includeSongs = false);

        void CreatePlaylist(Guid ownerId, Playlist playlist);

        IEnumerable<Song> GetSongs(string search);
        IEnumerable<SongWithPlaylistInfo> GetSongsFromPlaylist(Guid playlistId);
        Song GetSongFromPlaylist(Guid playlistId, Guid songId);
        void AddSongToPlaylist(Guid playlistId, Song song, int index);
        void UpdateSong(Song song);
        void DeleteSong(Song song);

        bool UserExists(Guid userId);
        IEnumerable<User> GetUsers();
        User GetUser(Guid userId, bool includePlaylists = false);
        IEnumerable<Playlist> GetPlaylistsFromUser(Guid userId);

        bool Save();
    }
}
