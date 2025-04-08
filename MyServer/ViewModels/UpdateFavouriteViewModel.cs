using MyServer.Models;

namespace MyServer.ViewModels
{
    internal class UpdateFavouriteViewModel: FavouriteViewModelBase
    {
        public Favourite SelectedFavourite { get; }

        public UpdateFavouriteViewModel(Favourite selectedFavourite)
        {
            SelectedFavourite = selectedFavourite;
            Name = SelectedFavourite.Name;
            FilePath = SelectedFavourite.FilePath;
            Arguments = SelectedFavourite.Arguments;
        }
    }
}
