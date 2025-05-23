﻿using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CreateModuleUserControl.xaml
    /// </summary>
    public partial class CreateModuleUserControl : UserControl
    {
        private readonly CreateModuleViewModel _viewModel;

        public CreateModuleUserControl()
        {
            InitializeComponent();
            _viewModel = new CreateModuleViewModel();
            DataContext = _viewModel;
        }

        private void CreateModule(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Field `Name` is required");
                return;
            }

            if (String.IsNullOrEmpty(_viewModel.Dir))
            {
                MessageBox.Show("Field `Directory` is required");
                return;
            }

            if (!Directory.Exists(_viewModel.Dir))
            {
                MessageBox.Show("Directory Not Found in `Dir`");
                return;
            }

            Module newModule = new()
            {
                Name = _viewModel.Name,
                Dir = _viewModel.Dir,
                Variable = _viewModel.Variable
            };

            // Проверяем перед добавлением
            if (ModuleStore.Instance.Modules.Any(s => s.Name == newModule.Name))
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            ModuleStore.Instance.AddModule(newModule);
            MessageBox.Show("Added Module");
        }

        private void OpenDialogDirectoryPath(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog openFolderDialog = new();

            if (openFolderDialog.ShowDialog() == true)
            {
                _viewModel.Dir = openFolderDialog.FolderName;
            }
        }
    }
}
