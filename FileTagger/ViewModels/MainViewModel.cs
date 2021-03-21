using FileTagger.Extensions;
using FileTagger.Interfaces;
using FileTagger.Interfaces.ViewModels;
using FileTagger.Resources;
using FileTagger.ViewModels.Docked;
using FileTagger.ViewModels.Menu;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FileTagger.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAppSettingsService appSettingsService;
        private readonly IFileSystemService fileSystemService;
        private string workingPath;

        public MainViewModel(
            IAppSettingsService appSettingsService,
            IFileSystemService fileSystemService,
            RibbonMenuViewModel ribbonMenuViewModel)
        {
            appSettingsService.CheckWhetherArgumentIsNull(nameof(appSettingsService));
            fileSystemService.CheckWhetherArgumentIsNull(nameof(fileSystemService));
            ribbonMenuViewModel.CheckWhetherArgumentIsNull(nameof(ribbonMenuViewModel));

            this.appSettingsService = appSettingsService;
            this.fileSystemService = fileSystemService;
            this.Ribbon = ribbonMenuViewModel;

            var workingDirectory = this.appSettingsService.GetSetting(Constants.WorkingDirectory);
            this.workingPath = workingDirectory?.SettingValue.ToString() ?? AppDomain.CurrentDomain.BaseDirectory;

            this.fileSystemService.SetWorkingDirectory(this.workingPath);

            this.FileExplorer = new FileExplorerViewModel(this.fileSystemService.ReadWorkingDirectory());

            this.SetupTabs();
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

        public FileExplorerViewModel FileExplorer { get; }

        public ICollection<IDockableViewModel> DockedViewModels { get; } = new ObservableCollection<IDockableViewModel>();

        private void SetupTabs()
        {
            this.DockedViewModels.Add(this.FileExplorer);
        }
    }
}
