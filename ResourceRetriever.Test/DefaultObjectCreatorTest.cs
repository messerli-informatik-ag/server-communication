using JetBrains.Annotations;
using System;
using Xunit;

namespace ResourceRetriever.Test
{
    public class DefaultObjectCreatorTest
    {
        [Fact]
        public void ReturnsDefault()
        {
            var creator = ResolveCreator();

            Assert.Equal(default(int), creator.CreateInstance<int>());
            Assert.Equal(default(string), creator.CreateInstance<string>());
            Assert.Equal(default(ObjectCreatorTestEnum), creator.CreateInstance<ObjectCreatorTestEnum>());
            Assert.Equal(default(ObjectCreatorTestClass), creator.CreateInstance<ObjectCreatorTestClass>());
        }

        [Fact]
        public void ReturnsEqualObject()
        {
            var creator = ResolveCreator();

            var expected = new ObjectCreatorTestClass(1, "Test", ObjectCreatorTestEnum.Three);
            var actual = creator.CreateInstance<ObjectCreatorTestClass>(new object[] { 1, "Test", ObjectCreatorTestEnum.Three });
            Assert.Equal(expected, actual);
        }

        public virtual IObjectCreator ResolveCreator()
        {
            return new DefaultObjectCreator();
        }

        #region TestStructures
        public enum ObjectCreatorTestEnum
        {
            [UsedImplicitly] One,
            [UsedImplicitly] Two,
            [UsedImplicitly] Three,
        }

        public class ObjectCreatorTestClass : IEquatable<ObjectCreatorTestClass>
        {
            public ObjectCreatorTestClass(int testInt, string testString, ObjectCreatorTestEnum testEnum)
            {
                TestInt = testInt;
                TestString = testString;
                TestEnum = testEnum;
            }

            public int TestInt { get; }

            public string TestString { get; }

            public ObjectCreatorTestEnum TestEnum { get; }

            #region manualy created equality functions

            public bool Equals(ObjectCreatorTestClass obj)
            {
                return TestInt.Equals(obj.TestInt)
                       && TestString.Equals(obj.TestString)
                       && TestEnum.Equals(obj.TestEnum);
            }

            public override bool Equals(object obj)
            {
                if (obj is ObjectCreatorTestClass instance)
                {
                    return Equals(instance);
                }

                return false;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = TestInt;
                    hashCode = (hashCode * 397) ^ (TestString != null ? TestString.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (int)TestEnum;
                    return hashCode;
                }
            }

            public static bool operator ==(ObjectCreatorTestClass left, ObjectCreatorTestClass right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(ObjectCreatorTestClass left, ObjectCreatorTestClass right)
            {
                return !Equals(left, right);
            }

            #endregion
        }

        #endregion
    }
}
