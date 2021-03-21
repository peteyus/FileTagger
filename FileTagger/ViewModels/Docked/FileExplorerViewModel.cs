using FileTagger.Extensions;
using FileTagger.Interfaces;
using FileTagger.Interfaces.ViewModels;
using FileTagger.Models.Nodes;
using GalaSoft.MvvmLight;

namespace FileTagger.ViewModels.Docked
{
    public class FileExplorerViewModel : ViewModelBase, IDockableViewModel
    {
        private NodeBase rootNode;

        public FileExplorerViewModel(NodeBase rootNode)
        {
            rootNode.CheckWhetherArgumentIsNull(nameof(rootNode));

            this.rootNode = rootNode;
        }

        public NodeBase RootNode
        {
            get => rootNode;

            set
            {
                rootNode = value;
                this.RaisePropertyChanged(nameof(this.RootNode));
            }
        }

        public string Title => "File Explorer";

        public string ContentId => "file-explorer";

        public bool CanClose => false;

        public bool IsSelected { get; set; }

        public bool IsActive { get; set; }
    }
}