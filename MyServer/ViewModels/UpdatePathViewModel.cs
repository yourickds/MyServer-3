using MyServer.Models;

namespace MyServer.ViewModels
{
    public class UpdatePathViewModel: PathViewModelBase
    {
        public Path SelectedPath { get; }

        public UpdatePathViewModel(Path selectedPath)
        {
            SelectedPath = selectedPath;
            Name = SelectedPath.Name;
            Dir = SelectedPath.Dir.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
