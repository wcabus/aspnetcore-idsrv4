using Microsoft.EntityFrameworkCore;

namespace Sprotify.API.Entities
{
    public class SprotifyContext : DbContext
    {
        // constructor required for migrations 
        public SprotifyContext(DbContextOptions<SprotifyContext> options)
               : base(options)
        {            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}
