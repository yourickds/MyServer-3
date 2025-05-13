using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyServer.ViewModels
{
    public class HostViewModelBase: INotifyPropertyChanged
    {
        private string? _name;
        private string? _ip;

        public string? Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string? Ip
        {
            get => _ip;
            set { _ip = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
