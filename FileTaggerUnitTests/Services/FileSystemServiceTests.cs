using FileTagger.Interfaces;
using FileTaggerUnitTests.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace FileTaggerUnitTests.Services
{
    [TestClass]
    public class FileSystemServiceTests : TestBase
    {
        private IFileSystemService classUnderTest;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            this.classUnderTest = this.CreateFileSystemService();

            this.MockFileSystem.MockDirectory.Setup(dir => dir.Exists(It.IsAny<string>())).Returns(true);
            this.MockFileSystem.MockPath.Setup(path => path.GetDirectoryName(It.IsAny<string>())).Returns((string str) => str);
            this.classUnderTest.SetWorkingDirectory("Someplace.");
        }

        [TestClass]
        public class ReadWorkingDirectoryTests : FileSystemServiceTests
        {
            [TestMethod]
            public void ReturnsNodeOfRootFolder()
            {
                // arrange
                string expectedNodeName = "Somebody set us up the bomb";
                var mockDirectory = new Mock<IDirectoryInfo>();
                mockDirectory.SetupGet(dir => dir.Name).Returns(expectedNodeName);

                this.MockFileSystem.MockDirectoryInfo.Setup(di => di.FromDirectoryName(It.IsAny<string>())).Returns(mockDirectory.Object);

                // act
                var result = this.classUnderTest.ReadWorkingDirectory();

                // assert
                Assert.AreEqual(expectedNodeName, result.Name, "Should have returned a node with the correct directory name.");
            }

            [TestMethod]
            public void ReturnsTreeOfNodes()
            {
                // arrange
                string expectedNodeName = "Somebody set us up the bomb";
                var fileNames = new List<string>();
                for (int i = 0; i < this.Random.Next(10); i++)
                {
                    fileNames.Add($"file {i}");
                }

                var mockDirectory = new Mock<IDirectoryInfo>();
                mockDirectory.SetupGet(dir => dir.Name).Returns(expectedNodeName);
                this.MockFileSystem.MockDirectory.Setup(dir => dir.GetFiles(It.IsAny<string>())).Returns(fileNames.ToArray());
                this.MockFileSystem.MockFileInfo.Setup(file => file.FromFileName(It.IsAny<string>()))
                    .Returns((string str) => 
                        { 
                            var fi = new Mock<IFileInfo>(); 
                            fi.Setup(info => info.Name).Returns(str); 
                            return fi.Object; 
                        });

                this.MockFileSystem.MockDirectoryInfo.Setup(di => di.FromDirectoryName(It.IsAny<string>())).Returns(mockDirectory.Object);

                // act
                var result = this.classUnderTest.ReadWorkingDirectory();

                // assert
                Assert.AreEqual(expectedNodeName, result.Name, "Should have returned a node with the correct directory name.");
                Assert.AreEqual(fileNames.Count, result.ChildNodes.Count, "Should have populated three child nodes.");
                foreach (string filename in fileNames)
                {
                    Assert.IsTrue(result.ChildNodes.Any(node => node.Name == filename), $"Expected to find {filename} in the nodes list but did not.");
                }
            }
        }
    }
}
