
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyServer.ViewModels
{
    class ModuleViewModelBase : INotifyPropertyChanged
    {
        private string? _name;
        private string? _dir;
        private string? _variable;

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

        public string? Variable
        {
            get => _variable;
            set { _variable = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
