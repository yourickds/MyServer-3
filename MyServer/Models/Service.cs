using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using MyServer.Actions;

namespace MyServer.Models
{
    public class Service: INotifyPropertyChanged
    {
        private int? _pid;
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string FilePath { get; set; }

        public string? Arguments { get; set; }

        public bool Startup { get; set; }

        [NotMapped]
        public string Status 
        {
            get { return GetStatusService.Invoke(this) ? "Запущена" : "Остановлена"; } 
        }

        public int? Pid 
        {
            get { return _pid; }
            set { _pid = value; OnPropertyChanged(); OnPropertyChanged(nameof(Status)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
