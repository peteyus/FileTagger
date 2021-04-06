using FileTagger.Models.Nodes;
using System;
using System.Windows.Input;

namespace FileTagger.Commands
{
    public class AddTagsCommand : CommandBase
    {   
        public override bool CanExecute(object parameter)
        {
            return parameter is FileNode;
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
