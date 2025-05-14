using System.Windows;
using Microsoft.EntityFrameworkCore;
using MyServer.Actions;
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
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Path> Paths { get; set; }
        public DbSet<Host> Hosts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                string appDirectory = GetMyServerDir.Invoke() + "\\MyDatabase.db";
                optionsBuilder.UseSqlite("Data Source=" + appDirectory);
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }
    }
}
