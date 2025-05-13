using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyServer.Models;
using MyServer.Stores;
using MyServer.UserControls;

namespace MyServer.ViewModels
{
    public class HostViewModel: INotifyPropertyChanged
    {
        private object _view = new CreateHostUserControl();

        private Host? _selectedHost = null;

        public ObservableCollection<Host> Hosts => HostStore.Instance.Hosts;

        public HostViewModel()
        {
            _view = new CreateHostUserControl();
            HostStore.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(HostStore.Hosts))
                {
                    OnPropertyChanged(nameof(Hosts));
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

        public Host? SelectedHost
        {
            get => _selectedHost;
            set
            {
                _selectedHost = value;
                View = _selectedHost != null
                    ? new UpdateHostUserControl(_selectedHost)
                    : new CreateHostUserControl();
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
