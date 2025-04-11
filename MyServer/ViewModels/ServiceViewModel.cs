using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyServer.Models;
using MyServer.Stores;
using MyServer.UserControls;

namespace MyServer.ViewModels
{
    class ServiceViewModel: INotifyPropertyChanged
    {
        private object _view = new CreateServiceUserControl();

        private Service? _selectedService = null;

        public ObservableCollection<Service> Services => ServiceStore.Instance.Services;

        public ServiceViewModel()
        {
            _view = new CreateServiceUserControl();
            ServiceStore.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ServiceStore.Services))
                {
                    OnPropertyChanged(nameof(Services));
                }
            };
        }

        public object View
        {
            get => _view;
            set
            {
                _view = value;
                OnPropertyChanged();
            }
        }

        public Service? SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                View = _selectedService != null
                    ? new UpdateServiceUserControl(_selectedService)
                    : new CreateServiceUserControl();
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
