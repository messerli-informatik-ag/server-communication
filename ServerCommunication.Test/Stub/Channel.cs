namespace ServerCommunication.Test.Stub
{
    [Equals]
    public class Channel
    {
        public Channel(string name, IReadOnlyCollection<Version> availableVersions)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            AvailableVersions = availableVersions ?? throw new ArgumentNullException(nameof(availableVersions));
        }

        public string Name { get; }

        public IReadOnlyCollection<Version> AvailableVersions { get; }
    }
}