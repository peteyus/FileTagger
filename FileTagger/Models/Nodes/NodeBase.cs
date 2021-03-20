using System.Collections.Generic;

namespace FileTagger.Models.Nodes
{
    public abstract class NodeBase
    {
        public string Name { get; set; }

        public string FullPath { get; set; }

        public string Icon { get; set; } // TODO PRJ: FontAwesome?

        public ICollection<NodeBase> ChildNodes { get; } = new List<NodeBase>();

        public ICollection<string> Tags { get; } = new List<string>();
    }
}
