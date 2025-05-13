using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MyServer.Models;

namespace MyServer.Stores
{
    public class PathStore: INotifyPropertyChanged
    {
        private static PathStore? _instance;
        public static PathStore Instance => _instance ??= new PathStore();

        private readonly Db _dbContext;

        private ObservableCollection<Path> _paths;

        public PathStore()
        {
            _dbContext = Db.Instance;
            _dbContext.Database.EnsureCreated();
            _paths = new ObservableCollection<Path>(_dbContext.Paths.ToList());
        }

        public ObservableCollection<Path> Paths
        {
            get => _paths;
            private set
            {
                _paths = value;
                OnPropertyChanged(nameof(Paths));
            }
        }

        public void AddPath(Path path)
        {
            path.Dir = Regex.Replace(path.Dir, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            _dbContext.Paths.Add(path);
            _dbContext.SaveChanges();
            RefreshPaths();
        }

        public void UpdatePath(Path path)
        {
            path.Dir = Regex.Replace(path.Dir, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            _dbContext.Paths.Update(path);
            _dbContext.SaveChanges();
            RefreshPaths();
        }

        public void DeletePath(int id)
        {
            var path = _dbContext.Paths.Find(id);
            if (path != null)
            {
                _dbContext.Paths.Remove(path);
                _dbContext.SaveChanges();
                RefreshPaths();
            }
        }

        private void RefreshPaths()
        {
            Paths = new ObservableCollection<Path>(_dbContext.Paths.ToList());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
