namespace ResourceRetriever
{
    public interface IDeserializer
    {
        T Deserialize<T>(string serializedData);
    }
}