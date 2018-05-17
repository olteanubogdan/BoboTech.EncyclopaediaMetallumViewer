using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class GetBandData
    {
        Guid _instanceId = Guid.NewGuid();

        public long Id { get; set; }

        public BandDetails Details { get; set; }

        [JsonProperty("band_name")]
        public string BandName { get; set; }

        public string Logo { get; set; }

        public string Photo { get; set; }

        public string Bio { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public List<Album> Discography { get; set; }

        [JsonProperty("current_lineup", NullValueHandling = NullValueHandling.Include)]
        public List<Member> CurrentLineup { get; set; }

        public override string ToString() => $"{nameof(GetBandData)} ({Id} - {_instanceId:N}): {nameof(BandName)} - {BandName}, {nameof(Discography)} - {Discography?.Count ?? 0}";
    }
}