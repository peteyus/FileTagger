using FileTagger.Extensions;

namespace FileTagger.Models.Nodes
{
    public class FileNode : NodeBase
    {
        public FileNode(string fileName, string fullPath)
        {
            fileName.CheckWhetherArgumentIsNull(nameof(fileName));
            fullPath.CheckWhetherArgumentIsNull(nameof(fullPath));

            this.Name = fileName;
            this.FullPath = fullPath;
        }
    }
}
