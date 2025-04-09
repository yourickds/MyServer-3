using MyServer.Models;

namespace MyServer.ViewModels
{
    internal class UpdateDomainViewModel: DomainViewModelBase
    {
        public Domain SelectedDomain { get; }

        public UpdateDomainViewModel(Domain selectedDomain)
        {
            SelectedDomain = selectedDomain;
            Name = SelectedDomain.Name;
            DocumentRoot = SelectedDomain.DocumentRoot;
            SelectedProfile = SelectedDomain.Profile;
        }
    }
}
