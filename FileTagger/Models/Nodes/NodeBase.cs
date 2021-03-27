using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Interop;
using System.Windows.Media;

namespace FileTagger.Models.Nodes
{
    public abstract class NodeBase
    {
        private ImageSource icon;

        public string Name { get; set; }

        public string FullPath { get; set; }

        public ImageSource Icon
        {
            get
            {
                if (this.icon == null && File.Exists(this.FullPath))
                {
                    using (Icon sysicon = System.Drawing.Icon.ExtractAssociatedIcon(this.FullPath))
                    {
                        this.icon = Imaging.CreateBitmapSourceFromHIcon(
                                  sysicon.Handle,
                                  System.Windows.Int32Rect.Empty,
                                  System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                    }
                }

                return this.icon;
            }
        }

        public ICollection<NodeBase> ChildNodes { get; } = new ObservableCollection<NodeBase>();

        public ICollection<string> Tags { get; } = new ObservableCollection<string>();
    }
}
