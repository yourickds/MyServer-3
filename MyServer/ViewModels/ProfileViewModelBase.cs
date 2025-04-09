using MyServer.Models;
using MyServer.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyServer.ViewModels
{
    internal class ProfileViewModelBase: INotifyPropertyChanged
    {
        private string? _name;

        public ObservableCollection<Module> Modules => ModuleStore.Instance.Modules;

        private Module? _selectedModule;

        private ObservableCollection<Module> _appendModules = [];

        private Module? _selectedAppendModule;

        public string? Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public ProfileViewModelBase()
        {
            ModuleStore.Instance.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(ModuleStore.Modules))
                {
                    OnPropertyChanged(nameof(Modules));
                }
            };
        }

        public Module? SelectedModule
        {
            get => _selectedModule;
            set { _selectedModule = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Module> AppendModules
        {
            get => _appendModules;
            set { _appendModules = value; OnPropertyChanged(); }
        }

        public Module? SelectedAppendModule
        {
            get => _selectedAppendModule;
            set { _selectedAppendModule = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
