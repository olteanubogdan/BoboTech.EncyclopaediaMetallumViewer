using DevExpress.Mvvm;

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

        public void GoBack()
        {
            if (this is ISupportParentViewModel supportParent)
                HostService.DataContext = supportParent.ParentViewModel;
        }

        public bool CanGoBack() => this is ISupportParentViewModel supportParent ? !(supportParent.ParentViewModel is null) : false;

        #endregion
    }
}