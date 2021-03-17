using FileTagger.Extensions;
using FileTagger.Interfaces;
using FileTagger.Resources;
using FileTagger.ViewModels.Menu;
using GalaSoft.MvvmLight;
using System;

namespace FileTagger.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAppSettingsService appSettingsService;
        private string workingPath;

        public MainViewModel(
            IAppSettingsService appSettingsService,
            RibbonMenuViewModel ribbonMenuViewModel)
        {
            appSettingsService.CheckWhetherArgumentIsNull(nameof(appSettingsService));
            ribbonMenuViewModel.CheckWhetherArgumentIsNull(nameof(ribbonMenuViewModel));

            this.appSettingsService = appSettingsService;
            this.Ribbon = ribbonMenuViewModel;

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

        public RibbonMenuViewModel Ribbon { get; }
    }
}
