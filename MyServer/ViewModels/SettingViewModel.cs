
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
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
            };
        }

        private string? _namePath;

        private Path? _selectedPath = null;

        public ObservableCollection<Path> Paths => new (
            SettingStore.Instance.Paths.Select(path => new Path
            {
                Id = path.Id,
                Name = path.Name.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory)
            })
        );

        public string? NamePath
        {
            get => _namePath;
            set { _namePath = value; OnPropertyChanged(); }
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
