
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.ViewModels
{
    public class SettingViewModel: INotifyPropertyChanged
    {
        public SettingViewModel()
        {
            SettingStore.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SettingStore.Hosts))
                {
                    OnPropertyChanged(nameof(Hosts));
                }
            };
        }

        private string? _domainHost;
        private string? _ipHost;

        private Host? _selectedHost = null;

        public ObservableCollection<Host> Hosts => SettingStore.Instance.Hosts;


        public string? DomainHost
        {
            get => _domainHost;
            set { _domainHost = value; OnPropertyChanged(); }
        }

        public string? IpHost
        {
            get => _ipHost;
            set { _ipHost = value; OnPropertyChanged(); }
        }


        public Host? SelectedHost
        {
            get => _selectedHost;
            set
            {
                _selectedHost = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
