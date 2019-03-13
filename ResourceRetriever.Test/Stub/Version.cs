namespace ResourceRetriever.Test.Stub
{
    [Equals]
    public class Version
    {
        public string Value { get; }

        public Version(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}