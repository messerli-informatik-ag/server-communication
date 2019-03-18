using System;

namespace Messerli.ServerCommunication
{
    public class ResolveTreeNode
    {
        public ResolveTreeNode(Type currentType, object current, ResolveTreeNode parent)
        {
            CurrentType = currentType;
            Current = current;
            Parent = parent;
        }

        public Type CurrentType { get; }

        public object Current { get; }

        public ResolveTreeNode Parent { get; }
    }
}
