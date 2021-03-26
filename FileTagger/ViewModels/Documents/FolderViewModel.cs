using FileTagger.Extensions;
using FileTagger.Interfaces;
using FileTagger.Interfaces.ViewModels;
using FileTagger.Models.Nodes;
using GalaSoft.MvvmLight;

namespace FileTagger.ViewModels.Documents
{
    public class FolderViewModel : ViewModelBase, IDocumentViewModel
    {
        private readonly IFileSystemService fileSystemService;
        private FolderNode currentDirectory;
        private bool isActive;
        private bool isSelected;

        public FolderViewModel(
            IFileSystemService fileSystemService,
            FolderNode currentDirectory = null)
        {
            fileSystemService.CheckWhetherArgumentIsNull(nameof(fileSystemService));

            this.fileSystemService = fileSystemService;
            this.currentDirectory = currentDirectory ?? this.fileSystemService.RootDirectoryTree;
        }

        public FolderNode CurrentDirectory
        {
            get => currentDirectory;

            set
            {
                currentDirectory = value;
                this.RaisePropertyChanged(nameof(CurrentDirectory));
            }
        }

        public string Title => this.CurrentDirectory?.Name;

        public bool CanClose => false; // TODO PRJ: Multiple folder tabs?

        public string ContentId => $"folder-view-{this.CurrentDirectory?.Name ?? "empty"}";

        public bool IsActive
        {
            get => isActive;

            set
            {
                isActive = value;
                this.RaisePropertyChanged(nameof(CurrentDirectory));
            }
        }

        public bool IsSelected
        {
            get => isSelected; 
            
            set
            {
                isSelected = value;
                this.RaisePropertyChanged(nameof(CurrentDirectory));
            }
        }

        public string ToolTip => this.CurrentDirectory?.FullPath;
    }
}
