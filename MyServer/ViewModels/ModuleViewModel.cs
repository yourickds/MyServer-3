using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyServer.Models;
using MyServer.Stores;
using MyServer.UserControls;

namespace MyServer.ViewModels
{
    class ModuleViewModel : INotifyPropertyChanged
    {
        private object _view = new CreateModuleUserControl();

        private Module? _selectedModule = null;

        public ObservableCollection<Module> Modules => ModuleStore.Instance.Modules;

        public ModuleViewModel()
        {
            _view = new CreateModuleUserControl();
            ModuleStore.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ModuleStore.Modules))
                {
                    OnPropertyChanged(nameof(Modules));
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

        public Module? SelectedModule
        {
            get => _selectedModule;
            set
            {
                _selectedModule = value;
                View = _selectedModule != null
                    ? new UpdateModuleUserControl(_selectedModule)
                    : new CreateModuleUserControl();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
