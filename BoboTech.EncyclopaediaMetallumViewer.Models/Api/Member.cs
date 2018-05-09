using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class Member
    {
        Guid _instanceId = Guid.NewGuid();

        public string Name { get; set; }

        public long Id { get; set; }

        public string Instrument { get; set; }

        public string Years { get; set; }

        public override string ToString() => $"{nameof(Member)} ({Id} - {_instanceId:N}): {nameof(Name)} - {Name}, {nameof(Instrument)} - {Instrument}, {nameof(Years)} - {Years}";
    }
}