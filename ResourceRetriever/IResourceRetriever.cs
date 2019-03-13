using System;
using System.Threading.Tasks;

namespace ResourceRetriever
{
    public interface IResourceRetriever
    {
        Task<T> RetrieveResource<T>(Uri uri);
    }
}