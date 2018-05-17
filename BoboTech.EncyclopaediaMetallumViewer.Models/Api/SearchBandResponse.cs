using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class SearchBandResponse : BaseResponse
    {
        public SearchBandData Data { get; set; }

        public override string ToString() => $"{nameof(SearchBandResponse)} ({_instanceId:N}): {nameof(Status)} - {Status}, {nameof(Code)} - {Code}, {nameof(Message)} - {Message}, {nameof(Hash)} - {Hash}";
    }
}