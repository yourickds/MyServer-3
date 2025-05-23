﻿using MyServer.Models;

namespace MyServer.ViewModels
{
    internal class UpdateProfileViewModel: ProfileViewModelBase
    {
        public Profile SelectedProfile { get; }

        public UpdateProfileViewModel(Profile selectedProfile)
        {
            SelectedProfile = selectedProfile;
            Name = SelectedProfile.Name;
            foreach (Module module in selectedProfile.Modules)
            {
                AppendModules.Add(module);
            }
        }
    }
}
