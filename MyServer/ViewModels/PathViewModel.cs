using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyServer.Models;
using MyServer.Stores;
using MyServer.UserControls;

namespace MyServer.ViewModels
{
    public class PathViewModel: INotifyPropertyChanged
    {
        private object _view = new CreatePathUserControl();

        private Path? _selectedPath = null;

        public ObservableCollection<Path> Paths => PathStore.Instance.Paths;

        public PathViewModel()
        {
            _view = new CreatePathUserControl();
            PathStore.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(PathStore.Paths))
                {
                    OnPropertyChanged(nameof(Paths));
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

        public Path? SelectedPath
        {
            get => _selectedPath;
            set
            {
                _selectedPath = value;
                View = _selectedPath != null
                    ? new UpdatePathUserControl(_selectedPath)
                    : new CreatePathUserControl();
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
