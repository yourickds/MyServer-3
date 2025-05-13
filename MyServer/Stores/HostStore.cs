using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyServer.Actions;
using MyServer.Models;

namespace MyServer.Stores
{
    public class HostStore: INotifyPropertyChanged
    {
        private static HostStore? _instance;

        public static HostStore Instance => _instance ??= new HostStore();

        private readonly Db _dbContext;

        private ObservableCollection<Host> _hosts;

        public HostStore()
        {
            _dbContext = Db.Instance;
            _dbContext.Database.EnsureCreated();
            _hosts = new ObservableCollection<Host>(_dbContext.Hosts.ToList());
        }

        public ObservableCollection<Host> Hosts
        {
            get => _hosts;
            private set
            {
                _hosts = value;
                OnPropertyChanged(nameof(Hosts));
            }
        }

        public void AddHost(Host host)
        {
            _dbContext.Hosts.Add(host);
            _dbContext.SaveChanges();
            RefreshHosts();

            ClearDomainHosts.Invoke();
            SetDomainHosts.Invoke();
        }

        public void UpdateHost(Host host)
        {
            _dbContext.Hosts.Update(host);
            _dbContext.SaveChanges();
            RefreshHosts();

            ClearDomainHosts.Invoke();
            SetDomainHosts.Invoke();
        }

        public void DeleteHost(int id)
        {
            var host = _dbContext.Hosts.Find(id);
            if (host != null)
            {
                RemoveLoopbackIp.Invoke(host.Ip);
                _dbContext.Hosts.Remove(host);
                _dbContext.SaveChanges();
                RefreshHosts();
                ClearDomainHosts.Invoke();
                SetDomainHosts.Invoke();
            }
        }

        private void RefreshHosts()
        {
            Hosts = new ObservableCollection<Host>(_dbContext.Hosts.ToList());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
