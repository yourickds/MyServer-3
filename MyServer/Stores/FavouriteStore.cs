using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MyServer.Models;

namespace MyServer.Stores
{
    internal class FavouriteStore : INotifyPropertyChanged
    {
        private static FavouriteStore? _instance;

        public static FavouriteStore Instance => _instance ??= new FavouriteStore();

        private readonly Db _dbContext;

        private ObservableCollection<Favourite> _favourites;

        public FavouriteStore()
        {
            _dbContext = new Db();
            _dbContext.Database.EnsureCreated();

            var favourites = _dbContext.Favourites.ToList();
            foreach (var favourite in favourites)
            {
                favourite.FilePath = favourite.FilePath.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
                favourite.Arguments = favourite.Arguments?.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
            }

            _favourites = new ObservableCollection<Favourite>(favourites);
        }

        public ObservableCollection<Favourite> Favourites
        {
            get => _favourites;
            private set
            {
                _favourites = value;
                OnPropertyChanged(nameof(Favourites));
            }
        }

        public void AddFavourite(Favourite favourite)
        {
            favourite.FilePath = Regex.Replace(favourite.FilePath, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            if (!String.IsNullOrEmpty(favourite.Arguments))
            {
                favourite.Arguments = Regex.Replace(favourite.Arguments, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            }
            _dbContext.Favourites.Add(favourite);
            _dbContext.SaveChanges();
            RefreshFavourites();
        }

        public void UpdateFavourite(Favourite favourite)
        {
            favourite.FilePath = Regex.Replace(favourite.FilePath, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            if (!String.IsNullOrEmpty(favourite.Arguments))
            {
                favourite.Arguments = Regex.Replace(favourite.Arguments, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            }
            _dbContext.Favourites.Update(favourite);
            _dbContext.SaveChanges();
            RefreshFavourites();
        }

        public void DeleteFavourite(int id)
        {
            var favourite = _dbContext.Favourites.Find(id);
            if (favourite != null)
            {
                _dbContext.Favourites.Remove(favourite);
                _dbContext.SaveChanges();
                RefreshFavourites();
            }
        }

        private void RefreshFavourites()
        {
            var favourites = _dbContext.Favourites.ToList();
            foreach (var favourite in favourites)
            {
                favourite.FilePath = favourite.FilePath.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
                favourite.Arguments = favourite.Arguments?.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
            }
            Favourites = new ObservableCollection<Favourite>(favourites);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
