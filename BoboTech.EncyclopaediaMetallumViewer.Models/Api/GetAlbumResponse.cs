using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class GetAlbumResponse : BaseResponse
    {
        public GetAlbumData Data { get; set; }

        public override string ToString() => $"{nameof(GetAlbumResponse)} ({_instanceId:N}): {nameof(Status)} - {Status}, {nameof(Code)} - {Code}, {nameof(Message)} - {Message}, {nameof(Hash)} - {Hash}";
    }
}