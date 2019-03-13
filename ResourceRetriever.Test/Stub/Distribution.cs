using System;
using System.Collections.Generic;

namespace ResourceRetriever.Test.Stub
{
    [Equals]
    public class Distribution
    {
        public Distribution(string name, IReadOnlyCollection<Channel> channels, IReadOnlyCollection<Modifier> modifiers, License license)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Channels = channels ?? throw new ArgumentNullException(nameof(channels));
            Modifiers = modifiers ?? throw new ArgumentNullException(nameof(modifiers));
            License = license;
        }

        public string Name { get; }

        public IReadOnlyCollection<Channel> Channels { get; }

        public IReadOnlyCollection<Modifier> Modifiers { get; }

        public License License { get; }
    }
}