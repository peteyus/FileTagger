using FileTagger.Interfaces;
using FileTagger.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace FileTaggerUnitTests.TestInfrastructure
{
    [TestClass]
    public class TestBase
    {
        // My services
        protected Mock<IFileSystemService> MockFileSystemService { get; private set; }

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


        private void InitializeMocks()
        {
            this.MockFileSystemService = new Mock<IFileSystemService>();
            this.MockFileSystem = new MockFileSystem();
        }
    }
}
