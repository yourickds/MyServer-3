using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyServer.Models;
using MyServer.Stores;
using MyServer.UserControls;

namespace MyServer.ViewModels
{
    internal class FavouriteViewModel : INotifyPropertyChanged
    {
        private object _view = new CreateFavouriteUserControl();

        private Favourite? _selectedFavourite = null;

        public ObservableCollection<Favourite> Favourites => FavouriteStore.Instance.Favourites;

        public FavouriteViewModel()
        {
            _view = new CreateFavouriteUserControl();
            FavouriteStore.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(FavouriteStore.Favourites))
                {
                    OnPropertyChanged(nameof(Favourites));
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

        public Favourite? SelectedFavourite
        {
            get => _selectedFavourite;
            set
            {
                _selectedFavourite = value;
                View = _selectedFavourite != null
                    ? new UpdateFavouriteUserControl(_selectedFavourite)
                    : new CreateFavouriteUserControl();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
