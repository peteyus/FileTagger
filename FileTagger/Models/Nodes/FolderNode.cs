using FileTagger.Extensions;

namespace FileTagger.Models.Nodes
{
    public class FolderNode : NodeBase
    {
        public FolderNode(string folderName, string fullPath)
        {
            folderName.CheckWhetherArgumentIsNull(nameof(folderName));
            fullPath.CheckWhetherArgumentIsNull(nameof(fullPath));

            this.Name = folderName;
            this.FullPath = fullPath;
        }
    }
}
