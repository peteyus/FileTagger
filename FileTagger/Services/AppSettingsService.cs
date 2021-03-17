using FileTagger.Interfaces;
using FileTagger.Models;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace FileTagger.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        public const string SettingsFileName = "filetagger.settings";

        private readonly ISerializationService serializationService;
        private readonly IFileSystem fileSystem;

        private IEnumerable<ApplicationSetting> allSettings;

        public AppSettingsService(ISerializationService serializationService, 
            IFileSystem fileSystem)
        {
            // TODO: Nullcheck arguments.
            this.serializationService = serializationService;
            this.fileSystem = fileSystem;

            this.ReadSettings();
        }

        public IEnumerable<ApplicationSetting> ReadSettings()
        {
            string settingsPath = this.fileSystem.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsFileName);
            if (!this.fileSystem.File.Exists(settingsPath))
            {
                this.allSettings = new List<ApplicationSetting>();
                return this.allSettings;
            }

            string settings = this.fileSystem.File.ReadAllText(settingsPath);
            try
            {
                var deserializedSettings = this.serializationService.DeserializeObject<IEnumerable<ApplicationSetting>>(settings);
                this.allSettings = deserializedSettings;
                return deserializedSettings;
            }
            catch (Exception)
            {
                this.allSettings = new List<ApplicationSetting>();
                return this.allSettings;
            }
        }

        public bool SaveSettings()
        {
            string settingsPath = this.fileSystem.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsFileName);
            try
            {
                string serializedSettings = this.serializationService.SerializeObject(this.allSettings);
                this.fileSystem.File.WriteAllText(settingsPath, serializedSettings);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object GetSettingValue(string setting)
        {
            if (string.IsNullOrWhiteSpace(setting))
            {
                throw new ArgumentNullException(nameof(setting));
            }

            var knownSetting = this.allSettings.FirstOrDefault(s => string.Equals(s.SettingName, setting, StringComparison.OrdinalIgnoreCase));
            if (knownSetting == null)
            {
                this.ReadSettings();
                knownSetting = this.allSettings.FirstOrDefault(s => string.Equals(s.SettingName, setting, StringComparison.OrdinalIgnoreCase));
            }

            if (knownSetting == null)
            {
                throw new InvalidOperationException($"Setting {setting} was not found.");
            }

            return knownSetting.SettingValue;
        }
    }
}
