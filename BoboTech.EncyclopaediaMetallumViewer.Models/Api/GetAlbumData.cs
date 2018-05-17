using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class GetAlbumData
    {
        Guid _instanceId = Guid.NewGuid();

        public AlbumBand Band { get; set; }

        public AlbumDetails Album { get; set; }

        public override string ToString() => $"{nameof(GetAlbumData)} ({_instanceId:N})";
    }
}