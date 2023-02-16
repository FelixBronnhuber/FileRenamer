using FileRenamer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FileRenamer.Commands
{
    public class FileExtensionCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        private readonly Regex _fileExtensionRegex = new Regex(@"\.[a-zA-Z0-9]+$");

        public FileExtensionCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_fileExtensionRegex.IsMatch((string) parameter)) {
                _mainViewModel.PreviewListBoxItems = new List<string>();
                foreach (string[] fileName in _mainViewModel.FileNamesList)
                {
                    _mainViewModel.PreviewListBoxItems.Add(fileName[0] + (string) parameter + " -> " + fileName[1] + (string) parameter);
                }

                _mainViewModel.IsValidCSVFileSelected = true;
            } else
            {
                MessageBox.Show("Invalid File Extension.");
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
