using System;
using static System.FormattableString;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class Band
    {
        Guid _instanceId = Guid.NewGuid();

        public string Name { get; set; }

        public long Id { get; set; }

        public string Genre { get; set; }

        public string Country { get; set; }

        public override string ToString() => Invariant($"{nameof(Band)} ({Id:d} - {_instanceId:N}): {nameof(Name)} - {Name}, {nameof(Genre)} - {Genre}, {nameof(Country)} - {Country}");
    }
}