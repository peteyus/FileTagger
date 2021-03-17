using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace FileTagger.ViewModels.Menu
{
    public class RibbonMenuViewModel : ViewModelBase
    {
        public RibbonMenuViewModel()
        {
            this.OpenCommand = new RelayCommand(this.OpenLocation);
            this.OptionsCommand = new RelayCommand(this.Options);
        }

        public ICommand OpenCommand { get; }

        public ICommand OptionsCommand { get; }

        private void OpenLocation()
        {

        }

        private void Options()
        {

        }
    }
}
