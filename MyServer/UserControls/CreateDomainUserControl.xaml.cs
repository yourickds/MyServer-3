﻿using System.Windows;
using System.Windows.Controls;
using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CreateDomainUserControl.xaml
    /// </summary>
    public partial class CreateDomainUserControl : UserControl
    {
        private readonly CreateDomainViewModel _viewModel;
        public CreateDomainUserControl()
        {
            InitializeComponent();
            _viewModel = new CreateDomainViewModel();
            DataContext = _viewModel;
        }

        private void CreateDomain(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Field `Name` is required");
                return;
            }

            // Проверка на допустимые символы
            if (!System.Text.RegularExpressions.Regex.IsMatch(_viewModel.Name, @"^[a-z0-9\-]+$"))
            {
                MessageBox.Show("Name can only contain lowercase letters, numbers and hyphens");
                return;
            }

            if (!String.IsNullOrEmpty(_viewModel.DocumentRoot))
            {
                // Проверка на допустимые символы (только a-z и /)
                if (!System.Text.RegularExpressions.Regex.IsMatch(_viewModel.DocumentRoot, @"^[a-z][a-z\/]*$"))
                {
                    MessageBox.Show("DocumentRoot can only contain lowercase letters and '/', and cannot start with '/'");
                    return;
                }

                if (_viewModel.DocumentRoot.EndsWith("/"))
                {
                    MessageBox.Show("DocumentRoot cannot end with '/'");
                    return;
                }

                if (_viewModel.DocumentRoot.Contains("//"))
                {
                    MessageBox.Show("DocumentRoot cannot contain double slashes '//'");
                    return;
                }
            }

            if (_viewModel.SelectedProfile is null or not Profile)
            {
                MessageBox.Show("Field `Profile` is required");
                return;
            }

            Domain newDomain = new()
            {
                Name = _viewModel.Name,
                DocumentRoot = _viewModel.DocumentRoot,
                ProfileId = _viewModel.SelectedProfile.Id,
                Profile = _viewModel.SelectedProfile
            };

            // Проверяем перед добавлением
            if (DomainStore.Instance.Domains.Any(s => s.Name == newDomain.Name))
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            DomainStore.Instance.AddDomain(newDomain);
            MessageBox.Show("Added Domain");
        }
    }
}
