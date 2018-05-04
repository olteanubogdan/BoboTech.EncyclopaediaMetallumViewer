using Newtonsoft.Json;

namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class Donation
    {
        public string Message { get; set; }

        [JsonProperty("donation_url")]
        public string DonationUrl { get; set; }

        public override string ToString() => $"{nameof(Donation)}: {nameof(Message)} - {Message}, {nameof(DonationUrl)} - {DonationUrl}";
    }
}