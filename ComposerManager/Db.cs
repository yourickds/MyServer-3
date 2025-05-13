using Microsoft.EntityFrameworkCore;
using ComposerManager.Models;
using ComposerManager.Actions;

namespace ComposerManager
{
    public class Db: DbContext
    {
        public DbSet<Module> Modules { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                string appDirectory = GetMyServerDir.Invoke() + "\\MyDatabase.db";
                optionsBuilder.UseSqlite("Data Source=" + appDirectory);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }
    }
}
