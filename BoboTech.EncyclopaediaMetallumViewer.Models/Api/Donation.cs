using Newtonsoft.Json;
using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class Donation
    {
        Guid _instanceId = Guid.NewGuid();

        public string Message { get; set; }

        [JsonProperty("donation_url")]
        public string DonationUrl { get; set; }

        public override string ToString() => $"{nameof(Donation)} ({_instanceId:N}): {nameof(Message)} - {Message}, {nameof(DonationUrl)} - {DonationUrl}";
    }
}