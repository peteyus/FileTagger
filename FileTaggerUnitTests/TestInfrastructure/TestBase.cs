using FileTagger.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace FileTaggerUnitTests.TestInfrastructure
{
    [TestClass]
    public class TestBase
    {
        protected Mock<IFileSystemService> MockFileSystemService { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            this.InitializeMocks();
        }

        private void InitializeMocks()
        {
            this.MockFileSystemService = new Mock<IFileSystemService>();
        }
    }
}
