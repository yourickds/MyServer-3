using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using MyServer.Actions;
using MyServer.Models;

namespace MyServer.Stores
{
    internal class DomainStore: INotifyPropertyChanged
    {
        private static DomainStore? _instance;
        public static DomainStore Instance => _instance ??= new DomainStore();

        private readonly Db _dbContext;

        private ObservableCollection<Domain> _domains;

        public DomainStore()
        {
            _dbContext = Db.Instance;
            _dbContext.Database.EnsureCreated();
            _domains = new ObservableCollection<Domain>(_dbContext.Domains.Include(d => d.Profile).ToList());
        }

        public ObservableCollection<Domain> Domains
        {
            get => _domains;
            private set
            {
                _domains = value;
                OnPropertyChanged(nameof(Domains));
            }
        }

        public void AddDomain(Domain domain)
        {
            _dbContext.Domains.Add(domain);
            _dbContext.SaveChanges();

            CreateTemplateConfigDomain.Invoke(domain);

            RefreshDomains();
        }

        public void UpdateDomain(Domain domain)
        {
            _dbContext.Domains.Update(domain);
            _dbContext.SaveChanges();
            RefreshDomains();
        }

        public void DeleteDomain(int id)
        {
            var domain = _dbContext.Domains.Find(id);
            if (domain != null)
            {
                _dbContext.Domains.Remove(domain);
                _dbContext.SaveChanges();
                RefreshDomains();
            }
        }

        private void RefreshDomains()
        {
            Domains = new ObservableCollection<Domain>(_dbContext.Domains.Include(d => d.Profile).ToList());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
