using Microsoft.EntityFrameworkCore;
using MyServer.Models;

namespace MyServer
{
    class Db: DbContext
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=MyDatabase.db");
        }
    }
}
