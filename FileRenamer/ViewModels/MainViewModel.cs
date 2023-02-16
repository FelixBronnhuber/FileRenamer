using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FileRenamer.Commands;

namespace FileRenamer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _csvFilePath = "no CSV file selected";
        private bool _isValidCSVFileSelected = false;
        private string _folderPath = "no Folder selected";
        private int _progressBarValue = 0;
        private bool _isSeperateFileExtensionComboBoxChecked = false;
        private string _fileExtensionText = ".txt";
        private int _selectedCSVConfigurationIndex = 0;
        private List<string> _previewListBoxItems = new List<string>();
        private List<string[]> _fileNamesList = new List<string[]>();

        public MainViewModel()
        {
            SelectCsvFileCommand = new SelectCSVFileCommand(this);
            SelectFolderCommand = new SelectFolderCommand(this);
            RenameFilesCommand = new RenameFilesCommand(this);
            FileExtensionCommand = new FileExtensionCommand(this);
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

        public bool IsValidCSVFileSelected { 
            get {
                return _isValidCSVFileSelected;
            }
            set
            {
                if (value == _isValidCSVFileSelected) return;
                _isValidCSVFileSelected = value;
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

        public bool IsSeperateFileExtensionComboBoxChecked
        {
            get => _isSeperateFileExtensionComboBoxChecked;
            set
            {
                if (value == _isSeperateFileExtensionComboBoxChecked) return;
                _isSeperateFileExtensionComboBoxChecked = value;
                OnPropertyChanged();
            }
        }

        public string FileExtensionText
        {
            get => _fileExtensionText;
            set
            {
                if (value == _fileExtensionText) return;
                _fileExtensionText = value;
                OnPropertyChanged();
            }
        }

        public int SelectedCSVConfigurationIndex
        {
            get => _selectedCSVConfigurationIndex;
            set
            {
                if (value == _selectedCSVConfigurationIndex) return;
                _selectedCSVConfigurationIndex = value;
                OnPropertyChanged();
            }
        }

        public List<string> PreviewListBoxItems
        {
            get => _previewListBoxItems;
            set
            {
                if (value == _previewListBoxItems) return;
                _previewListBoxItems = value;
                OnPropertyChanged();
            }
        }

        public List<string[]> FileNamesList
        {
            get => _fileNamesList;
            set
            {
                if (value == _fileNamesList) return;
                _fileNamesList = value;
            }
        }

        public SelectCSVFileCommand SelectCsvFileCommand { get; set; }
        public SelectFolderCommand SelectFolderCommand { get; set; }
        public RenameFilesCommand RenameFilesCommand { get; set; }
        public FileExtensionCommand FileExtensionCommand { get; set; }

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