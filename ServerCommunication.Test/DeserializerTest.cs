using Messerli.ServerCommunication.Test.Stub;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Version = Messerli.ServerCommunication.Test.Stub.Version;

namespace Messerli.ServerCommunication.Test
{
    public class DeserializerTest
    {
        [Theory]
        [ClassData(typeof(ValidJsonTestData))]
        public void DeserializesDistributions(string json, Distribution[] expectedDistributions)
        {
            var deserializer = ResolveDeserializer();
            var distributions = deserializer.Deserialize<IReadOnlyCollection<Distribution>>(json);

            Assert.Equal(expectedDistributions.Length, distributions.Count);
            Assert.True(expectedDistributions.SequenceEqual(distributions));
        }

        [Theory]
        [ClassData(typeof(InvalidJsonTestData))]
        public void ThrowsOnInvalidJson(string json)
        {
            var deserializer = ResolveDeserializer();
            Assert.Throws<ArgumentException>(() => deserializer.Deserialize<IReadOnlyCollection<Distribution>>(json));
        }

        private static IDeserializer ResolveDeserializer()
        {
            return new JsonDeserializer(new EnumerableObjectCreator());
        }

        public class ValidJsonTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    @"[ ]",
                    new Distribution[0]
                };

                yield return new object[]
                {
                @"[{
                    name: 'Foo',
                    channels: [],
                    modifiers: [],
                    license: 'None'
                }]",
                new [] {
                    new Distribution (
                        "Foo",
                        new Channel[0],
                        new Modifier[0],
                        License.None
                    )
                }
                };

                yield return new object[]
                {
                @"[{
                    name: 'Foo',
                    channels: [
                        {
                            name: 'standard',
                            availableVersions: [
                                { value: '1.2.3-alpha+build.234' }
                            ]
                        }
                    ],
                    modifiers: [],
                    license: 'None'
                }]",
                new [] {
                    new Distribution ("Foo", new[]
                        {
                            new Channel("standard", new[]
                                {
                                    new Version("1.2.3-alpha+build.234")
                                }
                            )
                        },
                        new Modifier[0],
                        License.None
                    )
                }
                };

                yield return new object[]
                {
                @"[{
                    name: 'Foo',
                    channels: [
                        {
                            name: 'standard',
                            availableVersions: [
                                { value: '0.1.2' },
                                { value: '1.2.3-alpha+build.234' },
                                { value: '2.3.3+sha256' }
                            ]
                        }
                    ],
                    modifiers: [],
                    license: 'None'
                }]",
                new [] {
                    new Distribution ("Foo", new[]
                        {
                            new Channel("standard", new[]
                                {
                                    new Version("0.1.2"),
                                    new Version("1.2.3-alpha+build.234"),
                                    new Version("2.3.3+sha256")
                                }
                            )
                        },
                        new Modifier[0],
                        License.None
                    )
                }
                };

                yield return new object[]
                {
                @"[{
                    name: 'Foo',
                    channels: [
                        {
                            name: 'standard',
                            availableVersions: [
                                { value: '0.1.2' },
                                { value: '1.2.3-alpha+build.234' },
                                { value: '2.3.3+sha256' }
                            ]
                        },
                        {
                            name: 'beta',
                            availableVersions: [
                                { value: '0.5.5-beta' },
                                { value: '42.3.48' }
                            ]
                        }
                    ],
                    modifiers: [],
                    license: 'Stale'
                }]",
                new [] {
                    new Distribution ("Foo", new[]
                        {
                            new Channel("standard", new[]
                                {
                                    new Version("0.1.2"),
                                    new Version("1.2.3-alpha+build.234"),
                                    new Version("2.3.3+sha256")
                                }
                            ),

                            new Channel("beta", new[]
                            {
                                new Version("0.5.5-beta"),
                                new Version("42.3.48")
                            })
                        },
                        new Modifier[0],
                        License.Stale
                    )
                }
                };

                yield return new object[]
                {
                @"[{
                    name: 'Foo',
                    channels: [],
                    modifiers: [
                        {
                            identifier: 'abc',
                            displayedName: 'Abc',
                        }
                    ],
                    license: 'Active'
                }]",
                new [] {
                    new Distribution (
                        "Foo",
                        new Channel[0],
                        new []
                        {
                            new Modifier("abc", "Abc"),
                        },
                        License.Active
                    )
                }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class InvalidJsonTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    @"{ Foo: 'Bar' }"
                };

                yield return new object[]
                {
                    @"[{ Foo: 'Bar' }]"
                };

                yield return new object[]
                {
                    @"{ }"
                };

                yield return new object[]
                {
                    @"[{ }]"
                };

                yield return new object[]
                {
                    @"foo#bar"
                };

                yield return new object[]
                {
                    @"[{ name: 'foo' }]"
                };

                yield return new object[]
                {
                    @"[{
                    name: 'foo',
                    availableVersions: [
                        { foo: 'bar' }
                    ]
                }]"
                };

                yield return new object[]
                {
                    @"[
                    {
                        name: 'foo',
                        availableVersions: [
                            { value: '1.2.3-alpha+build.234' }
                        ]
                    },
                    {
                        name: 'bar',
                        availableVersions: [
                            { foo: 'bar' }
                        ]
                    }
                ]"
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
