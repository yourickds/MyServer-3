using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MyServer.Actions;
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
            _modules = new ObservableCollection<Module>(_dbContext.Modules.ToList());
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

            //Получаем все профили которые используют данный модуль
            foreach(Profile profile in module.Profiles)
            {
                //Получаем список доменов которые привязаны к профилю и пересоздаем конфиги
                foreach (Domain domain in profile.Domains)
                {
                    // Удаляем конфиги .conf.tpl и .conf
                    DeleteConfigsDomain.Invoke(domain);
                    // Создаем .conf.tpl
                    CreateTemplateConfigDomain.Invoke(domain);
                    // Генерируем конфиг для домена
                    GenerateConfig.Invoke("userdata/configs/Apache24/vhosts/" + domain.Name + ".conf.tpl");
                }
            }

            // Перезапускаем службы
            RestartWorkServices.Invoke();
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
            Modules = new ObservableCollection<Module>(_dbContext.Modules.ToList());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
