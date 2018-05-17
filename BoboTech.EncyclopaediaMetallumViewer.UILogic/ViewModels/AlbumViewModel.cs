using BoboTech.EncyclopaediaMetallumViewer.Models.Api;
using BoboTech.EncyclopaediaMetallumViewer.UILogic.Extensions;
using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static System.FormattableString;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.ViewModels
{
    public class AlbumViewModel : BaseViewModel
    {
        #region Properties

        public virtual long Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string AlbumCover { get; set; }

        public virtual string AlbumType { get; set; }

        public virtual string ReleaseDate { get; set; }

        public virtual string CatalogId { get; set; }

        public virtual string Label { get; set; }

        public virtual string Format { get; set; }

        public virtual string Reviews { get; set; }

        public ObservableCollection<Song> Songs { get; } = new ObservableCollection<Song>();

        public ObservableCollection<Member> Personnel { get; } = new ObservableCollection<Member>();

        #endregion

        #region Public methods (will be transformed to commands by ViewModelSource)

        public async Task ViewLoadedAsync()
        {
            if (DataIsLoaded)
                return;

            var caller = $"{nameof(AlbumViewModel)}.{nameof(ViewLoadedAsync)}";
            try
            {
                BusyStatus = "Getting data ...";
                IsBusy = true;
                Songs.Clear();
                Personnel.Clear();
                var album = await Services.DataService.GetAlbumAsync(Id);
                IsBusy = false;
                album?.To(this);
                album?.Data?.Album?.To(this);

                if ((album?.Data?.Album?.Songs?.Count ?? 0) > 0)
                    album.Data.Album.Songs.ForEach(Songs.Add);

                if ((album?.Data?.Album?.Personnel?.Count ?? 0) > 0)
                    album.Data.Album.Personnel.ForEach(Personnel.Add);

                DataIsLoaded = true;
            }
            catch (Exception ex)
            {
                var errorId = Invariant($"{DateTime.Now:yyyyMMdd_HHmmss}");
                Logger.Log.Error(ex, "Failed to load band by id.", caller, errorId);
                IsBusy = false;
                MessageBoxService.ShowMessage($"Failed to get album details.{Environment.NewLine}{ex.Message}{Environment.NewLine}See log for more info. Error id is {errorId}.", WindowTitle, MessageButton.OK, MessageIcon.Error);
            }
        }

        public override string ToString() => $"{nameof(AlbumViewModel)} ({Id} - {_instanceId:N}): {nameof(Title)} - {Title}, {nameof(AlbumType)} - {AlbumType}, {nameof(ReleaseDate)} - {ReleaseDate}, {nameof(Songs)} - {Songs?.Count ?? 0}, {nameof(Personnel)} - {Personnel?.Count ?? 0}";

        #endregion
    }
}