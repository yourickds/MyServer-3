
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.ViewModels
{
    public class SettingViewModel: INotifyPropertyChanged
    {
        private string? _namePath;

        private Path? _selectedPath = null;

        public ObservableCollection<Path> Paths => SettingStore.Instance.Paths;
        public string? NamePath
        {
            get => _namePath;
            set { _namePath = value; OnPropertyChanged(); }
        }

        public SettingViewModel()
        {
            SettingStore.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SettingStore.Paths))
                {
                    OnPropertyChanged(nameof(Paths));
                }
            };
        }

        public Path? SelectedPath
        {
            get => _selectedPath;
            set
            {
                _selectedPath = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
