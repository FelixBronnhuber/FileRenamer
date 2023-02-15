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

            if (!File.Exists(_mainViewModel.CsvFileName))
            {
                MessageBox.Show("No valid CSV file selected.",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation
                );

                return;
            }

            if (!Directory.Exists(_mainViewModel.FolderName))
            {
                MessageBox.Show("No valid Folder selected.",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation
                );

                return;
            }

            List<string[]> fileNames = new List<string[]>();

            using (StreamReader reader = new StreamReader(_mainViewModel.CsvFileName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null) continue;

                    string[] values = line.Split(';');

                    if (values.Length != 2)
                        MessageBox.Show("The provided CSV File is invalid.",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );

                    fileNames.Add(values);
                }
            }

            int numItemsInList = fileNames.Count;

            await Task.Run(() =>
            {
                foreach (string[] fileName in fileNames)
                {
                    string oldFilePath = _mainViewModel.FolderName + "\\" + fileName[0];
                    string newFilePath = _mainViewModel.FolderName + "\\" + fileName[1];

                    if (File.Exists(oldFilePath) && !File.Exists(newFilePath))
                    {
                        File.Move(oldFilePath, newFilePath);

                        _mainViewModel.ProgressBarValue = 100 - fileNames.Count / numItemsInList * 100;
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