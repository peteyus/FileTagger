using FileTagger.Extensions;
using FileTagger.Interfaces;
using FileTagger.Models.Nodes;
using GalaSoft.MvvmLight;

namespace FileTagger.ViewModels.Docked
{
    public class FileExplorerViewModel : ViewModelBase
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
    }
}