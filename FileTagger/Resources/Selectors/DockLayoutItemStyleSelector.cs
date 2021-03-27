using FileTagger.Interfaces.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace FileTagger.Resources.Selectors
{
    public class DockLayoutItemStyleSelector : StyleSelector
    {
        public Style DocumentStyle { get; set; }

        public Style DockedPaneStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is IDocumentViewModel)
            {
                return this.DocumentStyle;
            }
            if (item is IDockableViewModel)
            {
                return this.DockedPaneStyle;
            }

            return base.SelectStyle(item, container);
        }
    }
}
