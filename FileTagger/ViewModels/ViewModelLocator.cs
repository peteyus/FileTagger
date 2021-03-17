using FileTagger.Interfaces;
using FileTagger.Services;
using System.IO.Abstractions;
using Unity;

namespace FileTagger.ViewModels
{
    public class ViewModelLocator
    {
        private IUnityContainer container;

        public ViewModelLocator()
        {
            this.RegisterExternalServices();
            this.RegisterInternalServices();
        }

        public IUnityContainer Container => this.container ?? (this.container = new UnityContainer());

        public MainViewModel Main => this.Container.Resolve<MainViewModel>();

        private void RegisterExternalServices()
        {
            this.Container.RegisterType<IFileSystem, FileSystem>(TypeLifetime.Singleton);
        }
        private void RegisterInternalServices()
        {
            this.Container.RegisterType<IFileSystemService, FileSystemService>(TypeLifetime.Singleton);
            this.Container.RegisterType<IAppSettingsService, AppSettingsService>(TypeLifetime.Singleton);
            this.Container.RegisterType<ISerializationService, SerializationService>(TypeLifetime.Singleton);
        }
    }
}
