using MyServer.Models;

namespace MyServer.ViewModels
{
    class UpdateModuleViewModel: ModuleViewModelBase
    {
        public Module SelectedModule { get; }

        public UpdateModuleViewModel(Module selectedModule)
        {
            SelectedModule = selectedModule;
            Name = SelectedModule.Name;
            Dir = SelectedModule.Dir;
        }
    }
}
