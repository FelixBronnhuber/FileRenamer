using System;
using System.IO;
using System.Windows.Input;
using FileRenamer.ViewModels;
using Microsoft.Win32;

namespace FileRenamer.Commands
{
    public class SelectFolderCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        private readonly OpenFileDialog _openFileDialog;

        public SelectFolderCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _openFileDialog = new OpenFileDialog
            {
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Select Folder",
                Filter = "Folders|no_files_allowed",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Select Folder"
            };
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_openFileDialog.ShowDialog() == true)
                _mainViewModel.FolderName = Path.GetDirectoryName(_openFileDialog.FileName);
        }

        public event EventHandler CanExecuteChanged;
    }
}