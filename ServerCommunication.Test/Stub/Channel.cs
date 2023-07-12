using System;
using System.Collections.Generic;

namespace Messerli.ServerCommunication.Test.Stub
{
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