namespace Messerli.ServerCommunication
{
    public interface IDeserializer
    {
        T Deserialize<T>(string serializedData);
    }
}