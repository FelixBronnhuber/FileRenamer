using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using FileRenamer.ViewModels;
using Microsoft.Win32;

namespace FileRenamer.Commands
{
    public class SelectCSVFileCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        private readonly OpenFileDialog _openFileDialog;

        public SelectCSVFileCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _openFileDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                FileName = "Select CSV File",
                Filter = "(*.csv)|*.csv",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Select CSV File"
            };
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_openFileDialog.ShowDialog() == true) _mainViewModel.CsvFileName = _openFileDialog.FileName;

            if (!File.Exists(_mainViewModel.CsvFileName))
            {
                MessageBox.Show("No valid CSV file selected.",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation
                );

                _mainViewModel.IsValidCSVFileSelected = false;
                _mainViewModel.PreviewListBoxItems = new List<string>();
                _mainViewModel.CsvFileName = "no CSV file selected";
                _mainViewModel.FileNamesList = new List<string[]>();
                return;
            }

            using (StreamReader reader = new StreamReader(_mainViewModel.CsvFileName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null) continue;

                    string[] values = line.Split(';');

                    if (values.Length != 2)
                    {
                        MessageBox.Show("The provided CSV File is invalid.",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );

                        return;
                    }

                    _mainViewModel.FileNamesList.Add(values);
                }
            }

            _mainViewModel.PreviewListBoxItems = new List<string>();
            foreach (string[] fileName in _mainViewModel.FileNamesList)
            {
                _mainViewModel.PreviewListBoxItems.Add(fileName[0] + " -> " + fileName[1]);
            }

            _mainViewModel.IsValidCSVFileSelected = true;
        }

        public event EventHandler CanExecuteChanged;
    }
}