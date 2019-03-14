namespace ServerCommunication
{
    public interface IResourceRetriever
    {
        Task<T> RetrieveResource<T>(Uri uri);
    }
}