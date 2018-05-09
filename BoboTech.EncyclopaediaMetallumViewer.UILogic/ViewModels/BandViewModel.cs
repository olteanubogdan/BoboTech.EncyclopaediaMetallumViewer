using DevExpress.Mvvm;
using System;
using System.Threading.Tasks;
using static System.FormattableString;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.ViewModels
{
    public class BandViewModel : BaseViewModel
    {
        #region Properties

        public virtual string Name { get; set; }

        public virtual long Id { get; set; }

        public virtual string Genre { get; set; }

        public virtual string Country { get; set; }

        #endregion

        #region Public methods (will be transformed to commands by ViewModelSource)

        public override string ToString() => Invariant($"{nameof(BandViewModel)} ({Id:d} - {_instanceId:N}): {nameof(Name)} - {Name}, {nameof(Genre)} - {Genre}, {nameof(Country)} - {Country}");

        public async Task ViewLoadedAsync()
        {
            var caller = $"{nameof(BandViewModel)}.{nameof(ViewLoadedAsync)}";
            try
            {
                var band = await Services.DataService.GetBandAsync(Id);
                Name = band?.Data?.BandName;
            }
            catch (Exception ex)
            {
                var errorId = Invariant($"{DateTime.Now:yyyyMMdd_HHmmss}");
                Logger.Log.Error(ex, "Failed to load band by id.", caller, errorId);
                MessageBoxService.ShowMessage($"Failed to search by band name. See log for more info. Error id is {errorId}.", WindowTitle, MessageButton.OK, MessageIcon.Error);
            }
        }

        #endregion
    }
}