﻿using FileTagger.Models;
using System.Collections.Generic;

namespace FileTagger.Interfaces
{
    public interface IAppSettingsService
    {
        IEnumerable<ApplicationSetting> ReadSettings();

        bool SaveSettings(IEnumerable<ApplicationSetting> settings);
    }
}
