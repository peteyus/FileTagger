using FileTagger.Interfaces;
using FileTagger.Models;
using System.Collections.Generic;

namespace FileTagger.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        public IEnumerable<ApplicationSetting> ReadSettings()
        {
            throw new System.NotImplementedException();
        }

        public bool SaveSettings(IEnumerable<ApplicationSetting> settings)
        {
            throw new System.NotImplementedException();
        }
    }
}
