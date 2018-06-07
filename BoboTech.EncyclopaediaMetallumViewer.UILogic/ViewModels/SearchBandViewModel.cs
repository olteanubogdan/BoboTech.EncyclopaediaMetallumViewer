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

        public virtual string TestBusyAsyncLabel { get; set; } = "Test busy";

        public virtual string LoadDummyDataLabel { get; set; } = "Load dummy data";

        #endregion

        #region Public methods (will be transformed to commands by ViewModelSource)

        public async Task SearchBandAsync()
        {
            var caller = $"{nameof(SearchBandViewModel)}({_instanceId:N}).{nameof(SearchBandAsync)}";
            var debug = new Action<string>(x => Logger.Log.Debug(x, caller));

            if (string.IsNullOrWhiteSpace(BandNameToSearch))
                return;

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

        [GenerateButton(Order = 0)]
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

        [GenerateButton(Order = 1)]
        public void LoadDummyData()
        {
            Bands.Clear();
            Bands.Add(new Band { Id = 1, Name = "Band 1", Genre = "Testing 123", Country = "Romania" });
            Bands.Add(new Band { Id = 2, Name = "Murica is the best", Genre = "C", Country = "'murica" });
            Bands.Add(new Band { Id = 3, Name = "Generic band name is so much generic wow", Genre = "Generic genre is generic", Country = "Generic country is generic" });
        }

        public override string ToString() => $"{nameof(SearchBandViewModel)} ({_instanceId:N}): {nameof(BandNameToSearch)} - {BandNameToSearch}, {nameof(EMApiCallStatus)} - {EMApiCallStatus}";

        #endregion
    }
}