
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MyServer.Models;

namespace MyServer.Stores
{
    public class SettingStore: INotifyPropertyChanged
    {
        private static SettingStore? _instance;
        public static SettingStore Instance => _instance ??= new SettingStore();

        private readonly Db _dbContext;

        private ObservableCollection<Path> _paths;

        public SettingStore()
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
            path.Name = Regex.Replace(path.Name, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            _dbContext.Paths.Add(path);
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
