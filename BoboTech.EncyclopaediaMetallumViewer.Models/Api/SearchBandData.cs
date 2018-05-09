using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static System.FormattableString;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class SearchBandData
    {
        Guid _instanceId = Guid.NewGuid();

        public string Query { get; set; }

        [JsonProperty(PropertyName = "search_results", NullValueHandling = NullValueHandling.Include)]
        public List<Band> SearchResults { get; set; }

        public override string ToString() => Invariant($"{nameof(SearchBandData)} ({_instanceId:N}): {nameof(Query)} - {Query}, {nameof(SearchResults)} - {SearchResults?.Count ?? 0:d}");
    }
}