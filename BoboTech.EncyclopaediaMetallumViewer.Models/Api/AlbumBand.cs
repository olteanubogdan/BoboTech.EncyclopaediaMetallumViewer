using Newtonsoft.Json;
using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class AlbumBand
    {
        Guid _instanceId = Guid.NewGuid();

        public long Id { get; set; }

        [JsonProperty("band_name")]
        public string BandName { get; set; }

        public override string ToString() => $"{nameof(AlbumBand)} ({Id} - {_instanceId:N}): {nameof(BandName)} - {BandName}";
    }
}