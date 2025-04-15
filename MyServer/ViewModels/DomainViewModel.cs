
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyServer.Models;
using MyServer.Stores;
using MyServer.UserControls;

namespace MyServer.ViewModels
{
    internal class DomainViewModel: INotifyPropertyChanged
    {
        private object _view = new CreateDomainUserControl();

        private Domain? _selectedDomain = null;

        public ObservableCollection<Domain> Domains => DomainStore.Instance.Domains;

        public DomainViewModel()
        {
            _view = new CreateDomainUserControl();
            DomainStore.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(DomainStore.Domains))
                {
                    OnPropertyChanged(nameof(Domains));
                }
            };
        }

        public object View
        {
            get => _view;
            set
            {
                _view = value;
                OnPropertyChanged();
            }
        }

        public Domain? SelectedDomain
        {
            get => _selectedDomain;
            set
            {
                _selectedDomain = value;
                View = _selectedDomain != null
                    ? new UpdateDomainUserControl(_selectedDomain)
                    : new CreateDomainUserControl();
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
