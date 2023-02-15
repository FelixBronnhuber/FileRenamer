using System;
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
        }

        public event EventHandler CanExecuteChanged;
    }
}