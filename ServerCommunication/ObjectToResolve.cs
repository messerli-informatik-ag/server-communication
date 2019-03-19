using System;

namespace Messerli.ServerCommunication
{
    public class ObjectToResolve
    {
        public ObjectToResolve(Type currentType, object current, ObjectToResolve parent)
        {
            CurrentType = currentType;
            Current = current;
            Parent = parent;
        }

        public Type CurrentType { get; }

        public object Current { get; }

        public ObjectToResolve Parent { get; }
    }
}
