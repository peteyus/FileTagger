using FileTagger.Interfaces;
using FileTagger.Models;
using FileTagger.Services;
using FileTaggerUnitTests.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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

        private ApplicationSetting SetupTestToReturnApplicationSetting(string settingName, object settingValue)
        {
            var expectedSetting = new ApplicationSetting { SettingName = settingName, SettingValue = settingValue };

            this.MockFileSystem.MockFile.Setup(file => file.Exists(It.IsAny<string>())).Returns(true);
            this.MockFileSystem.MockFile.Setup(file => file.ReadAllText(It.IsAny<string>())).Returns("all the settings");
            this.MockSerializationService.Setup(serialization => serialization.DeserializeObject<IEnumerable<ApplicationSetting>>(It.IsAny<string>()))
                .Returns(new List<ApplicationSetting>() { expectedSetting });

            return expectedSetting;
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
                    .Throws(new Exception("Can't get out that way kid."))
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
                var expectedSetting = this.SetupTestToReturnApplicationSetting("blah", 42);

                // act
                var result = this.classUnderTest.ReadSettings();

                // assert
                Assert.AreEqual(expectedSetting, result.First(), "Should have returned the expected setting.");
            }
        }

        [TestClass]
        public class SaveSettingsTests : AppSettingsServiceTests
        {
            [TestMethod]
            public void ReturnsFalseWhenSerializationFails()
            {
                // arrange
                this.MockSerializationService.Setup(serialization => serialization.SerializeObject(It.IsAny<object>()))
                    .Throws(new Exception("I'm afraid I can't do that Dave."))
                    .Verifiable("Should have attempted to serialize the settings.");

                // act
                var result = this.classUnderTest.SaveSettings();

                // assert
                Assert.IsFalse(result, "Should return false.");
                this.MockSerializationService.Verify();
            }

            [TestMethod]
            public void ReturnsFalseWhenSavingFileFails()
            {
                // arrange
                this.MockSerializationService.Setup(serialization => serialization.SerializeObject(It.IsAny<object>()))
                    .Returns("Totally serial.");
                this.MockFileSystem.MockFile.Setup(file => file.WriteAllText(It.IsAny<string>(), It.IsAny<string>()))
                    .Throws(new Exception("No. Just no."))
                    .Verifiable("Should have attempted to save the file.");

                // act
                var result = this.classUnderTest.SaveSettings();

                // assert
                Assert.IsFalse(result, "Should return false.");
                this.MockFileSystem.MockFile.Verify();
            }

            [TestMethod]
            public void ReturnsTrueWhenNoErrors()
            {
                // arrange
                this.MockSerializationService.Setup(serialization => serialization.SerializeObject(It.IsAny<object>()))
                    .Returns("Totally serial.")
                    .Verifiable("Should have attempted to serialize");
                this.MockFileSystem.MockFile.Setup(file => file.WriteAllText(It.IsAny<string>(), It.IsAny<string>()))
                    .Verifiable("Should have attempted to save the file.");

                // act
                var result = this.classUnderTest.SaveSettings();

                // assert
                Assert.IsTrue(result, "Should have succeeded.");
                this.MockSerializationService.Verify();
                this.MockFileSystem.MockFile.Verify();
            }
        }

        [TestClass]
        public class GetSettingValueTests : AppSettingsServiceTests
        {
            [TestMethod]
            public void ThrowsExceptionWhenNameIsNull()
            {
                // arrange
                try
                {
                    // act
                    this.classUnderTest.GetSettingValue(null);
                    Assert.Fail("Should have thrown an exception.");
                }
                catch (ArgumentNullException e)
                {
                    Assert.AreEqual("setting", e.ParamName, "Should have warned that the setting cannot be null.");
                }
            }

            [TestMethod]
            public void AttemptsToReadSettingsIfNameNotFound()
            {
                // arrange
                this.MockFileSystem.MockFile.Reset();
                this.SetupTestToReturnApplicationSetting("Bob", "boop");

                // act
                this.classUnderTest.GetSettingValue("Bob");

                // assert
                this.MockFileSystem.MockFile.Verify(file => file.Exists(It.IsAny<string>()),
                    Times.Once,
                    "Should have attempted to re-read the file.");
            }

            [TestMethod]
            public void DoesNotRequireCaseSensitiveMatch()
            {
                // arrange
                string something = "SoMeThInG";
                var expectedSetting = this.SetupTestToReturnApplicationSetting(something, "Something else.");

                // act
                var result = this.classUnderTest.GetSettingValue(something.ToLower());

                // assert
                Assert.AreEqual(expectedSetting.SettingValue, result, "Should have matched even with the case not matching.");
            }
        }
    }
}
