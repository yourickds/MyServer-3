using MyServer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyServer.Stores
{
    internal class ProfileStore: INotifyPropertyChanged
    {
        private static ProfileStore? _instance;

        public static ProfileStore Instance => _instance ??= new ProfileStore();

        private readonly Db _dbContext;

        private ObservableCollection<Profile> _profiles;

        public ProfileStore()
        {
            _dbContext = Db.Instance;
            _dbContext.Database.EnsureCreated();
            _profiles = new ObservableCollection<Profile>(_dbContext.Profiles.ToList());
        }

        public ObservableCollection<Profile> Profiles
        {
            get => _profiles;
            private set
            {
                _profiles = value;
                OnPropertyChanged(nameof(Profiles));
            }
        }

        public void AddProfile(Profile profile)
        {
            _dbContext.Profiles.Add(profile);
            _dbContext.SaveChanges();
            RefreshProfiles();
        }

        public void UpdateProfile(Profile profile)
        {
            _dbContext.Profiles.Update(profile);
            _dbContext.SaveChanges();
            RefreshProfiles();
        }

        public void DeleteProfile(int id)
        {
            var profile = _dbContext.Profiles.Find(id);
            if (profile != null)
            {
                _dbContext.Profiles.Remove(profile);
                _dbContext.SaveChanges();
                RefreshProfiles();
            }
        }

        private void RefreshProfiles()
        {
            Profiles = new ObservableCollection<Profile>(_dbContext.Profiles.ToList());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
