using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class Song
    {
        Guid _instanceId = Guid.NewGuid();

        public string Title { get; set; }

        public string Length { get; set; }

        public override string ToString() => $"{nameof(Song)} ({_instanceId:N}): {nameof(Title)} - {Title}, {nameof(Length)} - {Length}";
    }
}