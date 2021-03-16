using Moq;
using System.IO.Abstractions;

namespace FileTaggerUnitTests.TestInfrastructure
{
    public class MockFileSystem : IFileSystem
    {
        public MockFileSystem()
        {
            this.MockFile = new Mock<IFile>();
            this.MockDirectory = new Mock<IDirectory>();
            this.MockFileInfo = new Mock<IFileInfoFactory>();
            this.MockFileStream = new Mock<IFileStreamFactory>();
            this.MockPath = new Mock<IPath>();
            this.MockDirectoryInfo = new Mock<IDirectoryInfoFactory>();
            this.MockDriveInfo = new Mock<IDriveInfoFactory>();
            this.MockFileSystemWatcher = new Mock<IFileSystemWatcherFactory>();

            this.SetupMocks();
        }

        public Mock<IFile> MockFile { get; }

        public IFile File => this.MockFile.Object;

        public Mock<IDirectory> MockDirectory { get; }

        public IDirectory Directory => this.MockDirectory.Object;

        public Mock<IFileInfoFactory> MockFileInfo { get; }

        public IFileInfoFactory FileInfo => this.MockFileInfo.Object;

        public Mock<IFileStreamFactory> MockFileStream { get; }

        public IFileStreamFactory FileStream => this.MockFileStream.Object;

        public Mock<IPath> MockPath { get; }

        public IPath Path => this.MockPath.Object;

        public Mock<IDirectoryInfoFactory> MockDirectoryInfo { get; }

        public IDirectoryInfoFactory DirectoryInfo => this.MockDirectoryInfo.Object;

        public Mock<IDriveInfoFactory> MockDriveInfo { get; }

        public IDriveInfoFactory DriveInfo => this.MockDriveInfo.Object;

        public Mock<IFileSystemWatcherFactory> MockFileSystemWatcher { get; }

        public IFileSystemWatcherFactory FileSystemWatcher => this.MockFileSystemWatcher.Object;

        private void SetupMocks()
        {
            this.MockPath.Setup(path => path.Combine(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string a, string b) => System.IO.Path.Combine(a, b));
        }
    }
}
