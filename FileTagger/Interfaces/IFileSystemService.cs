using FileTagger.Models.Nodes;
using System.Collections.Generic;

namespace FileTagger.Interfaces
{
    public interface IFileSystemService
    {
        /// <summary>
        /// Returns the current working directory. Null if no working directory selected.
        /// </summary>
        string RootDirectory { get; }

        /// <summary>
        /// Returns a FolderNode containing the working tree under the root directory set by the last <see cref="ReadRootDirectory"/> call.
        /// </summary>
        FolderNode RootDirectoryTree { get; }

        /// <summary>
        /// Sets the current working directory.
        /// </summary>
        /// <param name="filePath">The path of a file or directory to work from. 
        ///     If a filename is provided, sets the working directory to the directory containing the specified file.</param>
        void SetRootDirectory(string filePath);

        NodeBase ReadRootDirectory();
    }
}
