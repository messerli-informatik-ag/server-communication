namespace ServerCommunication
{
    public interface IDeserializer
    {
        T Deserialize<T>(string serializedData);
    }
}