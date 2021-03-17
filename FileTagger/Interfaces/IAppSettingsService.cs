using FileTagger.Models;
using System.Collections.Generic;

namespace FileTagger.Interfaces
{
    public interface IAppSettingsService
    {
        IEnumerable<ApplicationSetting> ReadSettings();

        bool SaveSettings();

        ApplicationSetting GetSetting(string setting);
    }
}
