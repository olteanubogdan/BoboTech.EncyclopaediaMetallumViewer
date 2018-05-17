using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class AlbumDetails
    {
        Guid _instanceId = Guid.NewGuid();

        public long Id { get; set; }

        public string Title { get; set; }

        [JsonProperty("album_cover")]
        public string AlbumCover { get; set; }

        [JsonProperty("type")]
        public string AlbumType { get; set; }

        [JsonProperty("release date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("catalog id")]
        public string CatalogId { get; set; }

        public string Label { get; set; }

        public string Format { get; set; }

        public string Reviews { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public List<Song> Songs { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public List<Member> Personnel { get; set; }

        public override string ToString() => $"{nameof(AlbumDetails)} ({Id} - {_instanceId:N}): {nameof(Title)} - {Title}, {nameof(AlbumType)} - {AlbumType}, {nameof(ReleaseDate)} - {ReleaseDate}, {nameof(Songs)} - {Songs?.Count ?? 0}, {nameof(Personnel)} - {Personnel?.Count ?? 0}";
    }
}