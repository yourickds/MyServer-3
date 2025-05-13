
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
                if (e.PropertyName == nameof(SettingStore.Paths))
                {
                    OnPropertyChanged(nameof(Paths));
                }

                if (e.PropertyName == nameof(SettingStore.Hosts))
                {
                    OnPropertyChanged(nameof(Hosts));
                }
            };
        }

        private string? _namePath;

        private string? _domainHost;
        private string? _ipHost;

        private Path? _selectedPath = null;

        private Host? _selectedHost = null;

        public ObservableCollection<Path> Paths => new (
            SettingStore.Instance.Paths.Select(path => new Path
            {
                Id = path.Id,
                Name = path.Name.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory)
            })
        );

        public ObservableCollection<Host> Hosts => SettingStore.Instance.Hosts;

        public string? NamePath
        {
            get => _namePath;
            set { _namePath = value; OnPropertyChanged(); }
        }

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

        public Path? SelectedPath
        {
            get => _selectedPath;
            set
            {
                _selectedPath = value;
            }
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
