using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MyServer.Models;

namespace MyServer.Stores
{
    class ModuleStore : INotifyPropertyChanged
    {
        private static ModuleStore? _instance;

        public static ModuleStore Instance => _instance ??= new ModuleStore();

        private readonly Db _dbContext;

        private ObservableCollection<Module> _modules;

        public ModuleStore()
        {
            _dbContext = Db.Instance;
            _dbContext.Database.EnsureCreated();

            var modules = _dbContext.Modules.ToList();
            foreach (var module in modules)
            {
                module.Dir = module.Dir.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
            }

            _modules = new ObservableCollection<Module>(modules);
        }

        public ObservableCollection<Module> Modules
        {
            get => _modules;
            private set
            {
                _modules = value;
                OnPropertyChanged(nameof(Modules));
            }
        }

        public void AddModule(Module module)
        {
            module.Dir = Regex.Replace(module.Dir, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            _dbContext.Modules.Add(module);
            _dbContext.SaveChanges();
            RefreshModules();
        }

        public void UpdateModule(Module module)
        {
            module.Dir = Regex.Replace(module.Dir, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            _dbContext.Modules.Update(module);
            _dbContext.SaveChanges();
            RefreshModules();
        }

        public void DeleteModule(int id)
        {
            var module = _dbContext.Modules.Find(id);
            if (module != null)
            {
                _dbContext.Modules.Remove(module);
                _dbContext.SaveChanges();
                RefreshModules();
            }
        }

        private void RefreshModules()
        {
            var modules = _dbContext.Modules.ToList();
            foreach (var module in modules)
            {
                module.Dir = module.Dir.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
            }
            Modules = new ObservableCollection<Module>(modules);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
