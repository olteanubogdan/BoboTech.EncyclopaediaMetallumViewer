using BoboTech.EncyclopaediaMetallumViewer.Common;
using BoboTech.EncyclopaediaMetallumViewer.Common.Extensions;
using BoboTech.EncyclopaediaMetallumViewer.Models.Api;
using BoboTech.EncyclopaediaMetallumViewer.UILogic.Extensions;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static System.FormattableString;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.ViewModels
{
    public class SearchBandViewModel : BaseViewModel
    {
        #region Fields

        string _donationUrl;

        #endregion

        #region Properties

        public virtual string BandNameToSearch { get; set; }

        public virtual string DonationMessage { get; set; }

        public virtual string SearchStatus { get; set; }

        public virtual string SearchCode { get; set; }

        public virtual string SearchMessage { get; set; }

        public virtual string SearchHash { get; set; }

        public ObservableCollection<Band> Bands { get; } = new ObservableCollection<Band>();

        public virtual object SelectedBand { get; set; }

        #endregion

        #region Constructor

        public SearchBandViewModel()
        {
            if (!Debugger.IsAttached)
                return;

            Enumerable.Range(0, 20).ToList().ForEach(x => Bands.Add(new Band { Id = x, Country = $"Contry {x}", Genre = $"Genre {x}", Name = $"Band {x}" }));
        }

        #endregion

        #region Public methods (will be transformed to commands by ViewModelSource)

        public async Task SearchBand()
        {
            var caller = $"{nameof(SearchBandViewModel)}.{nameof(SearchBand)}";
            var debug = new Action<string>(x => Logger.Log.Debug(x, caller));
            try
            {
                Bands.Clear();

                string data = string.Empty;
                using (var client = new HttpClient())
                    data = await client.GetStringAsync(
                        new Uri(Settings.EncyclopaediaMetallum.BaseUrl)
                            .AddRelativeUri(Settings.EncyclopaediaMetallum.SearchBand)
                            .AddRelativeUri(BandNameToSearch)
                            .AddRelativeUri($"?api_key={Settings.EncyclopaediaMetallum.ApiKey}"));
                data.SaveForDebug(Invariant($"{DateTime.Now:yyyyMMdd_HHmmss_ffffff}_{Guid.NewGuid():N}.json"));
                var searchBand = data.SerializeJson<SearchBand>();
                debug($"No. of bands found: {searchBand?.Data?.SearchResults?.Count ?? 0}");
                (searchBand?.Data?.SearchResults ?? new List<Band>()).ForEach(Bands.Add);
                DonationMessage = searchBand?.Donation?.Message ?? string.Empty;
                _donationUrl = searchBand?.Donation?.DonationUrl ?? string.Empty;
                SearchStatus = searchBand?.Status;
                SearchCode = searchBand?.Code;
                SearchMessage = searchBand?.Message;
                SearchHash = searchBand?.Hash;
            }
            catch (Exception ex)
            {
                var errorId = Invariant($"{DateTime.Now:yyyyMMdd_HHmmss}");
                Logger.Log.Error(ex, "Failed to search by band name.", caller, errorId);
                MessageBoxService.ShowMessage($"Failed to search by band name. See log for more info. Error id is {errorId}.", WindowTitle, MessageButton.OK, MessageIcon.Error);
            }
        }

        public void OpenDonationUrl()
        {
            if (string.IsNullOrWhiteSpace(_donationUrl))
                return;

            Process.Start(_donationUrl);
        }

        public void ViewBand()
        {
            if (SelectedBand is Band band)
            {
                var bandViewModel = band.To<BandViewModel>();

                if (bandViewModel is ISupportParentViewModel supportParent)
                    supportParent.ParentViewModel = this;

                HostService.DataContext = bandViewModel;
            }
        }

        #endregion
    }
}