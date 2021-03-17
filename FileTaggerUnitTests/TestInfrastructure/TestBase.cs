using FileTagger.Interfaces;
using FileTagger.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO.Abstractions;

namespace FileTaggerUnitTests.TestInfrastructure
{
    [TestClass]
    public class TestBase
    {
        // My services
        protected Mock<IFileSystemService> MockFileSystemService { get; private set; }
        protected Mock<ISerializationService> MockSerializationService { get; private set; }

        // External services
        protected IFileSystem MockFileSystem { get; private set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            this.InitializeMocks();
        }

        protected IFileSystemService CreateFileSystemService()
        {
            return new FileSystemService(this.MockFileSystem);
        }

        protected IAppSettingsService CreateAppSettingsService()
        {
            return new AppSettingsService(this.MockSerializationService.Object);
        }

        private void InitializeMocks()
        {
            this.MockFileSystemService = new Mock<IFileSystemService>();
            this.MockSerializationService = new Mock<ISerializationService>();

            this.MockFileSystem = new MockFileSystem();
        }
    }
}
