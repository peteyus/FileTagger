using FileTagger.Extensions;
using FileTagger.Interfaces.ViewModels;
using GalaSoft.MvvmLight;
using System.IO.Abstractions;

namespace FileTagger.ViewModels.Documents
{
    public class FolderViewModel : ViewModelBase, IDocumentViewModel
    {
        private readonly IFileSystem fileSystem;
        private IDirectoryInfo currentDirectory;

        // TODO PRJ: Should I be using the FileSystemService? I think that implementation needs some thought.
        public FolderViewModel(IFileSystem fileSystem)
        {
            fileSystem.CheckWhetherArgumentIsNull(nameof(fileSystem));

            this.fileSystem = fileSystem;
        }

        public IDirectoryInfo CurrentDirectory 
        {
            get => currentDirectory; 
            
            set
            {
                currentDirectory = value;
                this.RaisePropertyChanged(nameof(this.CurrentDirectory));
            }
        }

        public string Title => throw new System.NotImplementedException();

        public bool CanClose => throw new System.NotImplementedException();

        public string ContentId => throw new System.NotImplementedException();

        public bool IsActive => throw new System.NotImplementedException();

        public bool IsSelected => throw new System.NotImplementedException();

        public string ToolTip => throw new System.NotImplementedException();
    }
}
