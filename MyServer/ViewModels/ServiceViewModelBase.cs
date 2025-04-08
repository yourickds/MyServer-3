using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyServer.ViewModels
{
    internal class ServiceViewModelBase: INotifyPropertyChanged
    {

        private string? _name;
        private string? _filePath;
        private string? _arguments;
        private bool _startup;

        public string? Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string? FilePath
        {
            get => _filePath;
            set { _filePath = value; OnPropertyChanged(); }
        }

        public string? Arguments
        {
            get => _arguments;
            set { _arguments = value; OnPropertyChanged(); }
        }

        public bool Startup
        {
            get => _startup;
            set { _startup = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
