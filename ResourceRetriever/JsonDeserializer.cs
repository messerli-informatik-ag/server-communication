using Newtonsoft.Json;
using System;
using Utility.Extension;

namespace ResourceRetriever
{
    public class JsonDeserializer : IDeserializer
    {
        private readonly JsonSerializerSettings _settings;
        private readonly IObjectCreator _objectCreator;
        private readonly IObjectResolver _objectResolver;

        public JsonDeserializer(IObjectCreator objectCreator, IObjectResolver objectResolver)
        {
            _objectCreator = objectCreator;
            _objectResolver = objectResolver;
            _settings = new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error };
        }

        public T Deserialize<T>(string serializedData)
        {
            var type = typeof(T);
            var isAnonymous = type.GetInnerType()?.IsAnonymous() ?? type.IsAnonymous();

            try
            {
                var deserializedObject = isAnonymous
                    ? JsonConvert.DeserializeAnonymousType(serializedData, _objectCreator.CreateInstance<T>())
                    : JsonConvert.DeserializeObject<T>(serializedData, _settings);

                return (T)_objectResolver.Resolve(deserializedObject);
            }
            catch (Exception exception) when (
                exception is JsonSerializationException ||
                exception is JsonReaderException)
            {
                throw new ArgumentException(exception.Message);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentException("Incomplete JSON object");
            }
        }
    }
}