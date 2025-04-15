using MyServer.Models;
using MyServer.Stores;
using MyServer.UserControls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MyServer.ViewModels
{
    internal class ProfileViewModel: INotifyPropertyChanged
    {
        private object _view = new CreateProfileUserControl();

        private Profile? _selectedProfile = null;

        public ObservableCollection<Profile> Profiles => ProfileStore.Instance.Profiles;

        public ProfileViewModel()
        {
            _view = new CreateProfileUserControl();
            ProfileStore.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ProfileStore.Profiles))
                {
                    OnPropertyChanged(nameof(Profiles));
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

        public Profile? SelectedProfile
        {
            get => _selectedProfile;
            set
            {
                _selectedProfile = value;
                View = _selectedProfile != null
                    ? new UpdateProfileUserControl(_selectedProfile)
                    : new CreateProfileUserControl();
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
