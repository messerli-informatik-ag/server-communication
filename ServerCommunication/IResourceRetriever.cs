using System;
using System.Threading.Tasks;

namespace Messerli.ServerCommunication
{
    public interface IResourceRetriever
    {
        Task<T> RetrieveResource<T>(Uri uri);
    }
}