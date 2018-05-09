using Newtonsoft.Json;
using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class BandDetails
    {
        Guid _instanceId = Guid.NewGuid();

        [JsonProperty("country of origin")]
        public string CountryOfOrigin { get; set; }

        public string Location { get; set; }

        public string Status { get; set; }

        [JsonProperty("formed in")]
        public int FormedIn { get; set; }

        public string Genre { get; set; }

        [JsonProperty("lyrical themes")]
        public string LyricalThemes { get; set; }

        [JsonProperty("current label")]
        public string CurrentLabel { get; set; }

        [JsonProperty("years active")]
        public string YearsActive { get; set; }

        public override string ToString() => $"{nameof(BandDetails)} ({_instanceId:N}): {nameof(CountryOfOrigin)} - {CountryOfOrigin}, {nameof(Location)} - {Location}, {nameof(Status)} - {Status}, {nameof(FormedIn)} - {FormedIn}, {nameof(Genre)} - {Genre}, {nameof(LyricalThemes)} - {LyricalThemes}, {nameof(CurrentLabel)} - {CurrentLabel}, {nameof(YearsActive)} - {YearsActive}";
    }
}