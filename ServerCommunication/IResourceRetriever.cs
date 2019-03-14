using System;
using System.Threading.Tasks;

namespace ServerCommunication
{
    public interface IResourceRetriever
    {
        Task<T> RetrieveResource<T>(Uri uri);
    }
}