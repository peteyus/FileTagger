using FileTagger.Interfaces;
using FileTagger.Models;
using FileTagger.Services;
using FileTaggerUnitTests.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

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

            [TestMethod]
            public void PassesFileContentsToSerializationService()
            {
                // arrange
                this.MockFileSystem.MockFile.Setup(file => file.Exists(It.IsAny<string>())).Returns(true);
                string fileContents = "blah blah blah.";
                this.MockFileSystem.MockFile.Setup(file => file.ReadAllText(It.IsAny<string>())).Returns(fileContents);

                // act
                this.classUnderTest.ReadSettings();

                // assert
                this.MockSerializationService.Verify(serialization => serialization.DeserializeObject<IEnumerable<ApplicationSetting>>(fileContents),
                    Times.Once,
                    "Should have attempted to deserialize the results.");
            }

            [TestMethod]
            public void DoesNotFailWhenDeserializationFails()
            {
                // arrange
                this.MockFileSystem.MockFile.Setup(file => file.Exists(It.IsAny<string>())).Returns(true);
                string fileContents = "blah blah blah.";
                this.MockFileSystem.MockFile.Setup(file => file.ReadAllText(It.IsAny<string>())).Returns(fileContents);
                this.MockSerializationService.Setup(serialization => serialization.DeserializeObject<IEnumerable<ApplicationSetting>>(It.IsAny<string>()))
                    .Throws(new System.Exception("Can't get out that way kid."))
                    .Verifiable("Should have attempted to deserialize");

                // act
                this.classUnderTest.ReadSettings();

                // assert -- no exceptions.
                this.MockSerializationService.Verify();
            }

            [TestMethod]
            public void ReturnsDeserializedSettings()
            {
                // arrange
                this.MockFileSystem.MockFile.Setup(file => file.Exists(It.IsAny<string>())).Returns(true);
                string fileContents = "blah blah blah.";
                var expectedSetting = new ApplicationSetting { SettingName = "blah", SettingValue = 42 };
                this.MockFileSystem.MockFile.Setup(file => file.ReadAllText(It.IsAny<string>())).Returns(fileContents);
                this.MockSerializationService.Setup(serialization => serialization.DeserializeObject<IEnumerable<ApplicationSetting>>(It.IsAny<string>()))
                    .Returns(new List<ApplicationSetting>() { expectedSetting } )
                    .Verifiable("Should have attempted to deserialize");

                // act
                var result = this.classUnderTest.ReadSettings();

                // assert
                this.MockSerializationService.Verify();
                Assert.AreEqual(expectedSetting, result.First(), "Should have returned the expected setting.");
            }
        }
    }
}
