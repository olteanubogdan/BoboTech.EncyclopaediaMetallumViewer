using BoboTech.EncyclopaediaMetallumViewer.Models.Api;
using BoboTech.EncyclopaediaMetallumViewer.UILogic.Extensions;
using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static System.FormattableString;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.ViewModels
{
    public class BandViewModel : BaseViewModel
    {
        #region Properties

        public virtual long Id { get; set; }

        public virtual string BandName { get; set; }

        public virtual string Logo { get; set; }

        public virtual string Photo { get; set; }

        public virtual string Bio { get; set; }

        public virtual string CountryOfOrigin { get; set; }

        public virtual string Location { get; set; }

        public virtual string Status { get; set; }

        public virtual string FormedIn { get; set; }

        public virtual string Genre { get; set; }

        public virtual string LyricalThemes { get; set; }

        public virtual string CurrentLabel { get; set; }

        public virtual string YearsActive { get; set; }

        public ObservableCollection<Album> Discography { get; } = new ObservableCollection<Album>();

        public virtual object SelectedAlbum { get; set; }

        public ObservableCollection<Member> CurrentLineup { get; } = new ObservableCollection<Member>();

        public virtual object SelectedMember { get; set; }

        #endregion

        #region Public methods (will be transformed to commands by ViewModelSource)

        public async Task ViewLoadedAsync()
        {
            if (DataIsLoaded)
                return;

            var caller = $"{nameof(BandViewModel)}.{nameof(ViewLoadedAsync)}";
            try
            {
                BusyStatus = "Getting data ...";
                IsBusy = true;
                Discography.Clear();
                CurrentLineup.Clear();
                var band = await Services.DataService.GetBandAsync(Id);
                IsBusy = false;
                band?.To(this);
                band?.Data?.To(this);
                band?.Data?.Details?.To(this);

                if ((band?.Data?.Discography?.Count ?? 0) > 0)
                    band.Data.Discography.ForEach(Discography.Add);

                if ((band?.Data?.CurrentLineup?.Count ?? 0) > 0)
                    band.Data.CurrentLineup.ForEach(CurrentLineup.Add);

                DataIsLoaded = true;
            }
            catch (Exception ex)
            {
                var errorId = Invariant($"{DateTime.Now:yyyyMMdd_HHmmss}");
                Logger.Log.Error(ex, "Failed to load band by id.", caller, errorId);
                IsBusy = false;
                MessageBoxService.ShowMessage($"Failed to get band details.{Environment.NewLine}{ex.Message}{Environment.NewLine}See log for more info. Error id is {errorId}.", WindowTitle, MessageButton.OK, MessageIcon.Error);
            }
        }

        public void ViewAlbum()
        {
            if (SelectedAlbum is Album album)
                HostService.ShowView(album.To<AlbumViewModel>());
        }

        public override string ToString() => $"{nameof(BandViewModel)} ({Id} - {_instanceId:N}): {nameof(BandName)} - {BandName}, {nameof(Genre)} - {Genre}, {nameof(CountryOfOrigin)} - {CountryOfOrigin}, {nameof(Discography)} - {Discography?.Count ?? 0}, {nameof(CurrentLineup)} - {CurrentLineup?.Count ?? 0}";

        #endregion
    }
}