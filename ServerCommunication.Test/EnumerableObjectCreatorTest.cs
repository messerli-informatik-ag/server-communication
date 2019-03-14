namespace ServerCommunication.Test
{
    public class EnumerableObjectCreatorTest : DefaultObjectCreatorTest
    {
        [Fact]
        public void ReturnsEnumerableObject()
        {
            var creator = ResolveCreator();

            Assert.Equal(new List<int> { default(int) }, creator.CreateInstance<List<int>>());
            Assert.Equal(new List<string> { default(string) }, creator.CreateInstance<List<string>>());
        }

        public override IObjectCreator ResolveCreator()
        {
            return new EnumerableObjectCreator();
        }
    }
}