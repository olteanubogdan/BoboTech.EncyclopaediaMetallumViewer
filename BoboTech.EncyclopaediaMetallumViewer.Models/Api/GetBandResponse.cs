using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class GetBandResponse : BaseResponse
    {
        public GetBandData Data { get; set; }

        public override string ToString() => $"{nameof(GetBandResponse)} ({_instanceId:N}): {nameof(Status)} - {Status}, {nameof(Code)} - {Code}, {nameof(Message)} - {Message}, {nameof(Hash)} - {Hash}";
    }
}