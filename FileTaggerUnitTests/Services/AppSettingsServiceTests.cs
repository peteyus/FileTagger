using FileTagger.Interfaces;
using FileTagger.Models;
using FileTagger.Services;
using FileTaggerUnitTests.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace FileTaggerUnitTests.Services
{
    [TestClass]
    public class AppSettingsServiceTests : TestBase
    {
        private IAppSettingsService classUnderTest;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            this.classUnderTest = this.CreateAppSettingsService();
        }

        [TestClass]
        public class ReadSettingsTests : AppSettingsServiceTests
        {
            [TestMethod]
            public void DoesNotFailIfFileDoesNotExist()
            {
                // arrange
                this.MockFileSystem.MockFile.Setup(file => file.Exists(It.IsAny<string>())).Returns(false);

                // act
                this.classUnderTest.ReadSettings();

                // assert
                this.MockFileSystem.MockFile.Verify(file => file.ReadAllText(It.IsAny<string>()),
                    Times.Never,
                    "Should not have attempted to read the nonexistant file.");
                this.MockSerializationService.Verify(serialization => serialization.DeserializeObject<IEnumerable<ApplicationSetting>>(It.IsAny<string>()), 
                    Times.Never, 
                    "Should not have attempted to deserialize anything.");
            }
        }
    }
}
