using FileTagger.Extensions;
using FileTagger.Interfaces;
using FileTagger.Models.Nodes;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace FileTagger.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IFileSystem fileSystem;

        public FileSystemService(IFileSystem fileSystem)
        {
            fileSystem.CheckWhetherArgumentIsNull(nameof(fileSystem));

            this.fileSystem = fileSystem;
        }

        public string WorkingDirectory { get; private set; }

        public void SetWorkingDirectory(string filePath)
        {
            filePath.CheckWhetherArgumentIsNull(nameof(filePath));

            if (!this.fileSystem.Directory.Exists(filePath))
            {
                throw new InvalidOperationException("That file path does not exist.");
            }

            this.WorkingDirectory = this.fileSystem.Path.GetDirectoryName(filePath);
        }

        public NodeBase ReadWorkingDirectory()
        {
            if (this.WorkingDirectory == null)
            {
                throw new InvalidOperationException("Cannot read with no working directory set.");
            }

            var rootDirectory = this.fileSystem.DirectoryInfo.FromDirectoryName(this.WorkingDirectory);
            FolderNode rootNode = new FolderNode(rootDirectory.Name, this.WorkingDirectory);
            this.PopulateChildDirectories(rootNode);

            return rootNode;
        }

        private void PopulateChildDirectories(NodeBase parentNode)
        {
            foreach (var childDirectory in this.fileSystem.Directory.GetDirectories(parentNode.FullPath))
            {
                var childNode = new FolderNode(this.fileSystem.DirectoryInfo.FromDirectoryName(childDirectory).Name, childDirectory);
                this.PopulateChildDirectories(childNode);
                this.PopulateChildFiles(childNode);
                parentNode.ChildNodes.Add(childNode);
            }

            this.PopulateChildFiles(parentNode);
        }

        private void PopulateChildFiles(NodeBase parentNode)
        {
            foreach (var childFile in this.fileSystem.Directory.GetFiles(parentNode.FullPath))
            {
                var childNode = new FileNode(this.fileSystem.FileInfo.FromFileName(childFile).Name, childFile);
                parentNode.ChildNodes.Add(childNode);
            }
        }
    }
}
