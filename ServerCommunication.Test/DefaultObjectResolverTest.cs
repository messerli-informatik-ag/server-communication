using System;
using System.Collections.Generic;
using Xunit;

namespace Messerli.ServerCommunication.Test
{
    public class DefaultObjectResolverTest
    {
        [Theory]
        [MemberData(nameof(GetTestObjects))]
        public void ReturnsObject(object obj)
        {
            var resolver = ResolveResolver();

            Assert.Equal(obj, resolver.Resolve(obj.GetType(), obj));
        }

        [Fact]
        public void ReturnsNull()
        {
            var resolver = ResolveResolver();

            Assert.Null(resolver.Resolve(typeof(int?), null));
            Assert.Null(resolver.Resolve(typeof(string), null));
        }

        [Fact]
        public void ThrowsOnNonNullableNulls()
        {
            var resolver = ResolveResolver();

            Assert.Throws<ArgumentException>(() => resolver.Resolve(typeof(int), null));
        }

        public virtual IObjectResolver ResolveResolver()
        {
            return new DefaultObjectResolver();
        }

        public static IEnumerable<object[]> GetTestObjects()
        {
            yield return new object[] { 2 };
            yield return new object[] { "Test" };
            yield return new object[] { new[] { 1, 2, 3, 4 } };
        }
    }
}