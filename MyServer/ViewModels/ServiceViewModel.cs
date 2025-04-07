using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.ViewModels
{
    class ServiceViewModel: INotifyPropertyChanged
    {
        private readonly ServiceStore _store;

        private ObservableCollection<Service> _services = [];

        public ServiceViewModel()
        {
            _store = new ServiceStore();
            Services = _store.GetAllServices();
        }

        public ObservableCollection<Service> Services
        {
            get => _services;
            set
            {
                _services = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
