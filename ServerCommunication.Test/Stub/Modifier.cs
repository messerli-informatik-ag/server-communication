namespace ServerCommunication.Test.Stub
{
    [Equals]
    public class Modifier
    {
        public string Identifier { get; }

        public string DisplayedName { get; }

        public Modifier(string identifier, string displayedName)
        {
            Identifier = identifier;
            DisplayedName = displayedName;
        }
    }
}