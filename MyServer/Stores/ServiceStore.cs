﻿using MyServer.Models;
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
            _services = new ObservableCollection<Service>(_dbContext.Services.ToList());

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
            Services = new ObservableCollection<Service>(_dbContext.Services.ToList());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
