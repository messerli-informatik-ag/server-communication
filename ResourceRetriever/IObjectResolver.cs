using System;

namespace ResourceRetriever
{
    public interface IObjectResolver
    {
        T Resolve<T>(T current);

        object Resolve(Type type, object current);
    }
}