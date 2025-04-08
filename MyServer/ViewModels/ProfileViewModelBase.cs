using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyServer.ViewModels
{
    internal class ProfileViewModelBase: INotifyPropertyChanged
    {
        private string? _name;

        public string? Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
