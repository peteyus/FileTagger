using FileTagger.Interfaces;
using FileTagger.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.IO.Abstractions;
using System.Reflection;

namespace IntegrationTests
{
    [TestClass]
    public class FileSystemServiceTests
    {
        private IFileSystemService classUnderTest;
        private IFileSystem fileSystem;

        [TestInitialize]
        public void TestInitialize()
        {
            this.fileSystem = new FileSystem();
            this.classUnderTest = new FileSystemService(this.fileSystem);
        }

        [TestClass]
        public class SetWorkingDirectoryTests : FileSystemServiceTests
        {
            [TestMethod]
            public void SetsDirectoryWhenGivenFileName()
            {
                var filePath = Assembly.GetExecutingAssembly().Location;
                var expectedDirectory = Path.GetDirectoryName(filePath);

                // act
                this.classUnderTest.SetRootDirectory(filePath);

                // assert
                Assert.AreEqual(expectedDirectory, this.classUnderTest.RootDirectory, "Should have set the working directory.");
            }
        }
    }
}
