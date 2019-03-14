namespace ServerCommunication
{
    public interface IObjectCreator
    {
        T CreateInstance<T>();

        T CreateInstance<T>(IEnumerable<object> parameters);

        object CreateInstance(Type type);

        object CreateInstance(Type type, IEnumerable<object> parameters);
    }
}