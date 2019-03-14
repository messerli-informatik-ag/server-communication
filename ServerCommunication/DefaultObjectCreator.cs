namespace ServerCommunication
{
    public class DefaultObjectCreator : IObjectCreator
    {
        public virtual T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
        }

        public virtual T CreateInstance<T>(IEnumerable<object> parameters)
        {
            return (T)CreateInstance(typeof(T), parameters);
        }

        public virtual object CreateInstance(Type type)
        {
            return !type.IsAnonymous()
                ? type.GetDefault()
                : CreateInstance(type, GetParameterTypes(GetConstructor(type)).Select(CreateInstance));
        }

        public virtual object CreateInstance(Type type, IEnumerable<object> parameters)
        {
            return parameters is null
                ? CreateInstance(type)
                : Activator.CreateInstance(type, parameters.ToArray());
        }

        internal static ConstructorInfo GetConstructor(Type type)
        {
            return type.GetConstructors().First();
        }

        internal static IEnumerable<Type> GetParameterTypes(ConstructorInfo constructor)
        {
            return constructor.GetParameters().Select(param => param.ParameterType);
        }
    }
}