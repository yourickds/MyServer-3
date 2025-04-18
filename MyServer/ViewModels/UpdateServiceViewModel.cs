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
            FilePath = SelectedService.FilePath.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
            Arguments = SelectedService.Arguments?.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
            Startup = SelectedService.Startup;
        }
    }
}
