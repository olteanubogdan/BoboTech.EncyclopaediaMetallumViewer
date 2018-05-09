using BoboTech.EncyclopaediaMetallumViewer.Common.Attributes;
using BoboTech.EncyclopaediaMetallumViewer.Models.Api;
using BoboTech.EncyclopaediaMetallumViewer.UILogic.Extensions;
using BoboTech.EncyclopaediaMetallumViewer.UILogic.Services;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static System.FormattableString;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.ViewModels
{
    public class SearchBandViewModel : BaseViewModel
    {
        #region Properties

        public virtual string BandNameToSearch { get; set; }

        public virtual string SearchStatus { get; set; }

        public virtual string SearchCode { get; set; }

        public virtual string SearchMessage { get; set; }

        public virtual string SearchHash { get; set; }

        public ObservableCollection<Band> Bands { get; } = new ObservableCollection<Band>();

        public virtual object SelectedBand { get; set; }

        public virtual string ShowTestLabel { get; set; } = "Test";

        #endregion

        #region Constructor

        //public SearchBandViewModel()
        //{
        //    if (!Debugger.IsAttached)
        //        return;
        //    Enumerable.Range(0, 20).ToList().ForEach(x => Bands.Add(new Band { Id = x, Country = $"Contry {x}", Genre = $"Genre {x}", Name = $"Band {x}" }));
        //}

        #endregion

        #region Public methods (will be transformed to commands by ViewModelSource)

        public async Task SearchBandAsync()
        {
            var caller = $"{nameof(SearchBandViewModel)}.{nameof(SearchBandAsync)}";
            var debug = new Action<string>(x => Logger.Log.Debug(x, caller));
            try
            {
                Bands.Clear();

                //SearchBand searchBand = null;
                //using (var memoryStream = new MemoryStream())
                //{
                //    using (var client = new HttpClient())
                //    {
                //        await (await client.GetStreamAsync(
                //            new Uri(Settings.EncyclopaediaMetallum.BaseUrl)
                //                .AddRelativeUri(Settings.EncyclopaediaMetallum.SearchBand)
                //                .AddRelativeUri(BandNameToSearch)
                //                .AddRelativeUri($"?api_key={Settings.EncyclopaediaMetallum.ApiKey}")
                //            )).CopyToAsync(memoryStream);
                //    }
                //    await memoryStream.SaveForDebug("json");
                //    searchBand = memoryStream.SerializeJson<SearchBand>();
                //}
                var searchBand = await DataService.SearchBandAsync(BandNameToSearch);
                debug($"No. of bands found: {searchBand?.Data?.SearchResults?.Count ?? 0}");
                (searchBand?.Data?.SearchResults ?? new List<Band>()).ForEach(Bands.Add);
                searchBand.To(this);
            }
            catch (Exception ex)
            {
                var errorId = Invariant($"{DateTime.Now:yyyyMMdd_HHmmss}");
                Logger.Log.Error(ex, "Failed to search by band name.", caller, errorId);
                MessageBoxService.ShowMessage($"Failed to search by band name. See log for more info. Error id is {errorId}.", WindowTitle, MessageButton.OK, MessageIcon.Error);
            }
        }

        public void ViewBand()
        {
            if (SelectedBand is Band band)
            {
                var vm = band.To<BandViewModel>();
                HostService.ShowView(vm);
            }
        }

        [GenerateButton(Order = 1, BindCommandTo = "ShowTestCommand", BindTextTo = nameof(ShowTestLabel))]
        public void ShowTest()
        {
            MessageBoxService.ShowMessage("Testing", WindowTitle);
        }

        public override string ToString() => $"{nameof(SearchBandViewModel)} ({_instanceId:N}): {nameof(BandNameToSearch)} - {BandNameToSearch}, {nameof(SearchStatus)} - {SearchStatus}";

        #endregion
    }
}