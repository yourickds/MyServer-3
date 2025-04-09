using Microsoft.EntityFrameworkCore;
using MyServer.Models;

namespace MyServer
{
    class Db: DbContext
    {
        private static Db? _instance;
        public static Db Instance => _instance ??= new Db();
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
