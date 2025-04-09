using MyServer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MyServer.Stores
{
    class ServiceStore: INotifyPropertyChanged
    {
        private static ServiceStore? _instance;

        public static ServiceStore Instance => _instance ??= new ServiceStore();

        private readonly Db _dbContext;

        private ObservableCollection<Service> _services;

        public ServiceStore()
        {
            _dbContext = Db.Instance;
            _dbContext.Database.EnsureCreated();

            var services = _dbContext.Services.ToList();
            foreach (var service in services)
            {
                service.FilePath = service.FilePath.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
                service.Arguments = service.Arguments?.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
            }
            _services = new ObservableCollection<Service>(services);

        }

        public ObservableCollection<Service> Services
        {
            get => _services;
            private set
            {
                _services = value;
                OnPropertyChanged(nameof(Services));
            }
        }

        public void AddService(Service service)
        {
            service.FilePath = Regex.Replace(service.FilePath, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            if (!String.IsNullOrEmpty(service.Arguments))
            {
                service.Arguments = Regex.Replace(service.Arguments, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            }
            _dbContext.Services.Add(service);
            _dbContext.SaveChanges();
            RefreshServices();
        }

        public void UpdateService(Service service)
        {
            service.FilePath = Regex.Replace(service.FilePath, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            if (!String.IsNullOrEmpty(service.Arguments))
            {
                service.Arguments = Regex.Replace(service.Arguments, Regex.Escape(AppDomain.CurrentDomain.BaseDirectory), "%myserverdir%\\", RegexOptions.IgnoreCase);
            }
            _dbContext.Services.Update(service);
            _dbContext.SaveChanges();
            RefreshServices();
        }

        public void DeleteService(int id)
        {
            var service = _dbContext.Services.Find(id);
            if (service != null)
            {
                _dbContext.Services.Remove(service);
                _dbContext.SaveChanges();
                RefreshServices();
            }
        }

        private void RefreshServices()
        {
            var services = _dbContext.Services.ToList();
            foreach (var service in services)
            {
                service.FilePath = service.FilePath.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
                service.Arguments = service.Arguments?.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
            }
            Services = new ObservableCollection<Service>(services);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
