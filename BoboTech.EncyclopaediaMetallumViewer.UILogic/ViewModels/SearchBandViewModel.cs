using BoboTech.EncyclopaediaMetallumViewer.Common.Attributes;
using BoboTech.EncyclopaediaMetallumViewer.Models.Api;
using BoboTech.EncyclopaediaMetallumViewer.UILogic.Extensions;
using BoboTech.EncyclopaediaMetallumViewer.UILogic.Services;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static System.FormattableString;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.ViewModels
{
    public class SearchBandViewModel : BaseViewModel
    {
        #region Properties

        public virtual string BandNameToSearch { get; set; }

        public ObservableCollection<Band> Bands { get; } = new ObservableCollection<Band>();

        public virtual object SelectedBand { get; set; }

        public virtual string TestBusyLabel { get; set; } = "Test busy";

        public virtual string TestResizeLabel { get; set; } = "Test resize";

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
                BusyStatus = "Searching ...";
                IsBusy = true;
                Bands.Clear();
                var searchBand = await DataService.SearchBandAsync(BandNameToSearch);
                debug($"No. of bands found: {searchBand?.Data?.SearchResults?.Count ?? 0}");
                (searchBand?.Data?.SearchResults ?? new List<Band>()).ForEach(Bands.Add);
                (searchBand as BaseResponse).To(this as BaseViewModel);
                IsBusy = false;
            }
            catch (Exception ex)
            {
                var errorId = Invariant($"{DateTime.Now:yyyyMMdd_HHmmss}");
                Logger.Log.Error(ex, "Failed to search by band name.", caller, errorId);
                IsBusy = false;
                MessageBoxService.ShowMessage($"Failed to search by band name.{Environment.NewLine}{ex.Message}{Environment.NewLine}See log for more info. Error id is {errorId}.", WindowTitle, MessageButton.OK, MessageIcon.Error);
            }
        }

        public void ViewBand()
        {
            if (SelectedBand is Band band)
                HostService.ShowView(band.To<BandViewModel>());
        }

        [GenerateButton(Order = 0, BindCommandTo = "TestBusyAsyncCommand", BindTextTo = nameof(TestBusyLabel))]
        public async Task TestBusyAsync()
        {
            BusyStatus = "Testing.";
            IsBusy = true;
            await Task.Delay(1000);
            BusyStatus = "Almost done.";
            await Task.Delay(1000);
            BusyStatus = "Done.";
            IsBusy = false;
        }

        [GenerateButton(Order = 1, BindCommandTo = "TestResizeCommand", BindTextTo = nameof(TestResizeLabel))]
        public void TestResize()
        {
            EMApiCallMessage = "Some long test that, hopefully, should resize the content of Encyclopaedia Metallum API call message.";
        }

        public override string ToString() => $"{nameof(SearchBandViewModel)} ({_instanceId:N}): {nameof(BandNameToSearch)} - {BandNameToSearch}, {nameof(EMApiCallStatus)} - {EMApiCallStatus}";

        #endregion
    }
}