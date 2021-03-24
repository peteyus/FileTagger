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
        private string documentRoot;

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

            var previousDocumentRoot = this.appSettingsService.GetSetting(Constants.DocumentRoot);
            this.documentRoot = previousDocumentRoot?.SettingValue.ToString() ?? AppDomain.CurrentDomain.BaseDirectory;

            this.fileSystemService.SetWorkingDirectory(this.documentRoot);

            this.FileExplorer = new FileExplorerViewModel(this.fileSystemService.ReadWorkingDirectory());

            this.SetupTabs();
        }

        public string DocumentRoot
        {
            get => documentRoot;
            set
            {
                documentRoot = value;
                this.RaisePropertyChanged(nameof(this.DocumentRoot));
            }
        }

        public RibbonMenuViewModel Ribbon { get; }

        public FileExplorerViewModel FileExplorer { get; }

        public ICollection<IDockableViewModel> DockedViewModels { get; } = new ObservableCollection<IDockableViewModel>();

        public ICollection<IDocumentViewModel> Documents { get; } = new ObservableCollection<IDocumentViewModel>();

        private void SetupTabs()
        {
            this.DockedViewModels.Add(this.FileExplorer);
        }
    }
}
