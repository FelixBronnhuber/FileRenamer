using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FileRenamer.Commands;

namespace FileRenamer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _csvFilePath = "no CSV file selected";
        private string _folderPath = "no Folder selected";
        private int _progressBarValue;

        public MainViewModel()
        {
            SelectCsvFileCommand = new SelectCSVFileCommand(this);
            SelectFolderCommand = new SelectFolderCommand(this);
            RenameFilesCommand = new RenameFilesCommand(this);
        }

        public string CsvFileName
        {
            get => _csvFilePath;
            set
            {
                if (value == _csvFilePath) return;
                _csvFilePath = value;
                OnPropertyChanged();
            }
        }

        public string FolderName
        {
            get => _folderPath;
            set
            {
                if (value == _folderPath) return;
                _folderPath = value;
                OnPropertyChanged();
            }
        }

        public int ProgressBarValue
        {
            get => _progressBarValue;
            set
            {
                if (value == _progressBarValue) return;
                _progressBarValue = value;
                OnPropertyChanged();
            }
        }

        public SelectCSVFileCommand SelectCsvFileCommand { get; set; }
        public SelectFolderCommand SelectFolderCommand { get; set; }
        public RenameFilesCommand RenameFilesCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}