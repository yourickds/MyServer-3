using MyServer.Models;
using System.Collections.ObjectModel;

namespace MyServer.Stores
{
    class ServiceStore
    {
        private readonly Db _dbContext;

        public ServiceStore()
        {
            _dbContext = new Db();
            _dbContext.Database.EnsureCreated();
        }

        public ObservableCollection<Service> GetAllServices()
        {
            return new ObservableCollection<Service>([.. _dbContext.Services]);
        }
    }
}
