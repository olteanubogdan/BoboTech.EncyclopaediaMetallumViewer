using Newtonsoft.Json;
using System.Collections.Generic;
using static System.FormattableString;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class SearchBandData
    {
        public string Query { get; set; }

        [JsonProperty(PropertyName = "search_results", NullValueHandling = NullValueHandling.Include)]
        public List<Band> SearchResults { get; set; }

        public override string ToString() => Invariant($"{nameof(SearchBandData)}: {nameof(Query)} - {Query}, {nameof(SearchResults)} - {SearchResults?.Count ?? 0:d}");
    }
}