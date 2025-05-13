using MyServer.Models;

namespace MyServer.ViewModels
{
    public class UpdateHostViewModel: HostViewModelBase
    {
        public Host SelectedHost { get; }

        public UpdateHostViewModel(Host selectedHost)
        {
            SelectedHost = selectedHost;
            Name = SelectedHost.Name;
            Ip = SelectedHost.Ip;
        }
    }
}
