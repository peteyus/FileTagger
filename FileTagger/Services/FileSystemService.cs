using FileTagger.Interfaces;
using System;
using System.IO.Abstractions;

namespace FileTagger.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IFileSystem fileSystem;

        public FileSystemService(IFileSystem fileSystem)
        {
            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }

            this.fileSystem = fileSystem;
        }

        public string WorkingDirectory { get; private set; }

        public void SetWorkingDirectory(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!this.fileSystem.File.Exists(filePath))
            {
                throw new InvalidOperationException("That file path does not exist.");
            }

            this.WorkingDirectory = this.fileSystem.Path.GetDirectoryName(filePath);
        }
    }
}
