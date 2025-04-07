using Microsoft.EntityFrameworkCore;
using MyServer.Models;

namespace MyServer
{
    class Db: DbContext
    {
        public DbSet<Service> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=MyDatabase.db");
        }
    }
}
