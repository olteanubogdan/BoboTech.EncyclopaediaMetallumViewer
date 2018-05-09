using Newtonsoft.Json;
using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class Album
    {
        Guid _instanceId = Guid.NewGuid();

        public string Title { get; set; }

        public long Id { get; set; }

        [JsonProperty("type")]
        public string AlbumType { get; set; }

        public int Year { get; set; }

        public override string ToString() => $"{nameof(Album)} ({Id} - {_instanceId:N}): {nameof(Title)} - {Title}, {nameof(Year)} - {Year}, {nameof(AlbumType)} - {AlbumType}";
    }
}