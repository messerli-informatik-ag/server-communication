using System;

namespace Messerli.ServerCommunication
{
    public class ResolveObject
    {
        public ResolveObject(Type currentType, object current, ResolveObject parent)
        {
            CurrentType = currentType;
            Current = current;
            Parent = parent;
        }

        public Type CurrentType { get; }

        public object Current { get; }

        public ResolveObject Parent { get; }
    }
}
