
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyServer.ViewModels
{
    class ModuleViewModelBase : INotifyPropertyChanged
    {
        private string? _name;
        private string? _dir;

        public string? Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string? Dir
        {
            get => _dir;
            set { _dir = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
