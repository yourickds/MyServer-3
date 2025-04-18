using MyServer.Actions;
using MyServer.Models;

namespace MyServer.ViewModels
{
    class UpdateServiceViewModel: ServiceViewModelBase
    {
        private bool _status;
        private int? _pid;

        public Service SelectedService { get; }

        public int? Pid
        {
            get { return _pid; }
            set { _pid = value; Status = GetStatusService.Invoke(SelectedService);  OnPropertyChanged(); }
        }

        public bool Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }
        }

        public UpdateServiceViewModel(Service selectedService)
        {
            SelectedService = selectedService;
            Name = SelectedService.Name;
            FilePath = SelectedService.FilePath.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
            Arguments = SelectedService.Arguments?.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
            Startup = SelectedService.Startup;
            Pid = SelectedService.Pid;
            Status = GetStatusService.Invoke(SelectedService);
        }
    }
}
