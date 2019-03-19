using System;

namespace Messerli.ServerCommunication
{
    public class ObjectToResolve
    {
        public ObjectToResolve(Type type, object current, ObjectToResolve parent)
        {
            Type = type;
            Current = current;
            Parent = parent;
        }

        public Type Type { get; }

        public object Current { get; }

        public ObjectToResolve Parent { get; }
    }
}
