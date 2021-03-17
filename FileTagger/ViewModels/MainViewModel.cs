using FileTagger.Extensions;
using FileTagger.Interfaces;
using FileTagger.Resources;
using GalaSoft.MvvmLight;
using System;

namespace FileTagger.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAppSettingsService appSettingsService;
        private string workingPath;

        public MainViewModel(IAppSettingsService appSettingsService)
        {
            appSettingsService.CheckWhetherArgumentIsNull(nameof(appSettingsService));

            this.appSettingsService = appSettingsService;

            var workingDirectory = this.appSettingsService.GetSetting(Constants.WorkingDirectory);
            this.workingPath = workingDirectory?.SettingValue.ToString() ?? AppDomain.CurrentDomain.BaseDirectory;
        }

        public string WorkingPath
        {
            get => workingPath;
            set 
            { 
                workingPath = value; 
                this.RaisePropertyChanged(nameof(this.WorkingPath)); 
            } 
        }
    }
}
