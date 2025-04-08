using MyServer.Models;

namespace MyServer.ViewModels
{
    class UpdateServiceViewModel: ServiceViewModelBase
    {
        public Service SelectedService { get; }

        public UpdateServiceViewModel(Service selectedService)
        {
            SelectedService = selectedService;
            Name = SelectedService.Name;
            FilePath = SelectedService.FilePath;
            Arguments = SelectedService.Arguments;
            Startup = SelectedService.Startup;
        }
    }
}
