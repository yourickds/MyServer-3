using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.ViewModels
{
    internal class DomainViewModelBase: INotifyPropertyChanged
    {
        private string? _name;

        private string? _documentRoot;
        public ObservableCollection<Profile> Profiles => ProfileStore.Instance.Profiles;

        private Profile? _selectedProfile;

        public string? Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string? DocumentRoot
        {
            get => _documentRoot;
            set { _documentRoot = value; OnPropertyChanged(); }
        }

        public DomainViewModelBase()
        {
            ProfileStore.Instance.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(ProfileStore.Profiles))
                {
                    OnPropertyChanged(nameof(Profiles));
                }
            };
        }

        public Profile? SelectedProfile
        {
            get => _selectedProfile;
            set { _selectedProfile = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
