using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace ServerCommunication.Test
{
    public class HttpResourceRetrieverTest
    {
        [Fact]
        public async void RetrievesResource()
        {
            var httpClient = MockHttpClient();

            var deserializer = Substitute.For<IDeserializer>();
            deserializer.Deserialize<IReadOnlyCollection<string>>(Response).Returns(ExpectedResource);

            var resourceRetriever = new HttpResourceRetriever(httpClient, deserializer);
            var resource = await resourceRetriever.RetrieveResource<IReadOnlyCollection<string>>(ResourceUri);

            Assert.Equal(ExpectedResource, resource);
        }

        [Fact]
        public async void ThrowsOnUnavailableResource()
        {
            var httpClient = new MockHttpMessageHandler().ToHttpClient();

            var deserializer = Substitute.For<IDeserializer>();
            deserializer.Deserialize<IReadOnlyCollection<string>>(Response).Returns(ExpectedResource);

            var resourceRetriever = new HttpResourceRetriever(httpClient, deserializer);

            await Assert.ThrowsAsync<UnavailableResourceException>(() => resourceRetriever.RetrieveResource<IReadOnlyCollection<string>>(ResourceUri));
        }

        [Fact]
        public async void ThrowsOnFailedDeserializationResource()
        {
            var httpClient = MockHttpClient();

            var deserializer = Substitute.For<IDeserializer>();
            deserializer
                .Deserialize<IReadOnlyCollection<string>>(Response)
                .Throws(new ArgumentException("Deserialization failed"));

            var resourceRetriever = new HttpResourceRetriever(httpClient, deserializer);

            await Assert.ThrowsAsync<ArgumentException>(() => resourceRetriever.RetrieveResource<IReadOnlyCollection<string>>(ResourceUri));
        }

        private static HttpClient MockHttpClient()
        {
            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler.When(ResourceUri.ToString()).Respond("application/json", Response);
            return mockHttpMessageHandler.ToHttpClient();
        }

        private static Uri ResourceUri => new Uri("https://foo.com");

        private const string Response = @"[""bar"", ""baz""]";

        private static IReadOnlyCollection<string> ExpectedResource => new[] { "bar", "baz" };
    }
}