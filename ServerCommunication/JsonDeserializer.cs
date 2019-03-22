using Messerli.Utility.Extension;
using Newtonsoft.Json;
using System;

namespace Messerli.ServerCommunication
{
    public class JsonDeserializer : IDeserializer
    {
        private readonly JsonSerializerSettings _settings;
        private readonly IObjectCreator _objectCreator;

        public JsonDeserializer(IObjectCreator objectCreator)
        {
            _objectCreator = objectCreator;
            _settings = new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error };
        }

        public T Deserialize<T>(string serializedData)
        {
            var type = typeof(T);
            var isAnonymous = type.GetInnerType()?.IsAnonymous() ?? type.IsAnonymous();

            try
            {
                return isAnonymous
                    ? JsonConvert.DeserializeAnonymousType(serializedData, _objectCreator.CreateInstance<T>())
                    : JsonConvert.DeserializeObject<T>(serializedData, _settings);
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