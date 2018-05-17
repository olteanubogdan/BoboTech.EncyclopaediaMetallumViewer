using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class BaseResponse
    {
        protected Guid _instanceId = Guid.NewGuid();

        public string Status { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }

        public string Hash { get; set; }

        public Donation Donation { get; set; }

        public override string ToString() => $"{nameof(BaseResponse)} ({_instanceId:N}): {nameof(Status)} - {Status}, {nameof(Code)} - {Code}, {nameof(Message)} - {Message}, {nameof(Hash)} - {Hash}";
    }
}