using FileTagger.Interfaces;
using FileTagger.Services;
using FileTaggerUnitTests.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        }
    }
}
