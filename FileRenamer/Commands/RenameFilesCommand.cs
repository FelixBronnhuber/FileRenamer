using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FileRenamer.ViewModels;

namespace FileRenamer.Commands
{
    public class RenameFilesCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        private bool _isExecuting;

        public RenameFilesCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return !_isExecuting;
        }

        public async void Execute(object parameter)
        {
            _isExecuting = true;

            if (_mainViewModel.FileNamesList.Count < 1)
            {
                MessageBox.Show("No valid CSV file selected.",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation
                );

                _isExecuting = false;
                return;
            }

            if (!Directory.Exists(_mainViewModel.FolderName))
            {
                MessageBox.Show("No valid Folder selected.",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation
                );

                _isExecuting = false;
                return;
            }

            int numItemsInList = _mainViewModel.FileNamesList.Count;

            await Task.Run(() =>
            {
                foreach (string[] fileName in _mainViewModel.FileNamesList)
                {
                    string oldFilePath = _mainViewModel.FolderName + "\\" + fileName[0];
                    string newFilePath = _mainViewModel.FolderName + "\\" + fileName[1];

                    if (File.Exists(oldFilePath) && !File.Exists(newFilePath))
                    {
                        File.Move(oldFilePath, newFilePath);

                        _mainViewModel.ProgressBarValue = 100 - _mainViewModel.FileNamesList.Count / numItemsInList * 100;
                    }
                    else
                    {
                        MessageBox.Show(
                            "Could not rename file \""
                            + oldFilePath
                            + "\" to \""
                            + newFilePath
                            + "\".\nEither the old file does not exist or the new file already exists.",
                            "Warning",
                            MessageBoxButton.OK,
                            MessageBoxImage.Exclamation
                        );
                    }
                }
            });

            _mainViewModel.ProgressBarValue = 100;
            _isExecuting = false;
        }

        public event EventHandler CanExecuteChanged;
    }
}