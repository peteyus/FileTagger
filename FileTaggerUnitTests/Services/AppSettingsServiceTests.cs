using FileTagger.Interfaces;
using FileTagger.Services;
using FileTaggerUnitTests.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            this.classUnderTest = new AppSettingsService();
        }
    }
}
