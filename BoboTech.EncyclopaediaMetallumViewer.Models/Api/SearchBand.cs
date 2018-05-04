namespace BoboTech.EncyclopaediaMetallumViewer.Models.Api
{
    public class SearchBand
    {
        public string Status { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }

        public string Hash { get; set; }

        public Donation Donation { get; set; }

        public SearchBandData Data { get; set; }

        public override string ToString() => $"{nameof(SearchBand)}: {nameof(Status)} - {Status}, {nameof(Code)} - {Code}, {nameof(Message)} - {Message}, {nameof(Hash)} - {Hash}";
    }
}