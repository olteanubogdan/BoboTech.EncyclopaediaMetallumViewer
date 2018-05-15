using BoboTech.EncyclopaediaMetallumViewer.Common.Attributes;
using BoboTech.EncyclopaediaMetallumViewer.Common.Services;
using DevExpress.Mvvm;
using System;
using System.Diagnostics;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        #region Fields

        protected Guid _instanceId = Guid.NewGuid();

        #endregion

        #region Properties

        protected IMessageBoxService MessageBoxService => GetService<IMessageBoxService>();

        protected IHostService HostService => GetService<IHostService>();

        public virtual string WindowTitle { get; set; } = "Encyclopaedia Metallum Viewer";

        public virtual string GoBackLabel { get; set; } = "Back";

        public virtual string EMApiDonationUrl { get; set; }

        public virtual string EMApiDonationMessage { get; set; }

        public virtual string EMApiCallStatus { get; set; }

        public virtual string EMApiCallCode { get; set; }

        public virtual string EMApiCallMessage { get; set; }

        public virtual string EMApiCallHash { get; set; }

        public virtual bool IsBusy { get; set; }

        public virtual string BusyStatus { get; set; }

        #endregion

        #region Public methods (will be transformed to commands by ViewModelSource)

        [GenerateButton(Order = 0, BindCommandTo = "GoBackCommand", BindTextTo = nameof(GoBackLabel))]
        public void GoBack() => HostService.GoBack();

        public bool CanGoBack() => HostService?.CanGoBack() ?? false;

        public void OpenEMApiDonationUrl()
        {
            if (string.IsNullOrWhiteSpace(EMApiDonationUrl))
                return;

            Process.Start(EMApiDonationUrl);
        }

        #endregion
    }
}