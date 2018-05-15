using BoboTech.EncyclopaediaMetallumViewer.Common.Attributes;
using BoboTech.EncyclopaediaMetallumViewer.Common.Services;
using DevExpress.Mvvm;
using System;
using System.Diagnostics;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase, ISupportChildViewModel
    {
        #region Fields

        protected Guid _instanceId = Guid.NewGuid();

        #endregion

        #region Properties

        protected IMessageBoxService MessageBoxService => GetService<IMessageBoxService>();

        protected IHostService HostService => GetService<IHostService>();

        public virtual string WindowTitle { get; set; } = "Encyclopaedia Metallum Viewer";

        public virtual string GoBackLabel { get; set; } = "Back";

        public virtual string GoForwardLabel { get; set; } = "Forward";

        public virtual string EMApiDonationUrl { get; set; }

        public virtual string EMApiDonationMessage { get; set; }

        public virtual string EMApiCallStatus { get; set; }

        public virtual string EMApiCallCode { get; set; }

        public virtual string EMApiCallMessage { get; set; }

        public virtual string EMApiCallHash { get; set; }

        public virtual bool IsBusy { get; set; }

        public virtual string BusyStatus { get; set; }

        public object ChildViewModel { get; set; }

        #endregion

        #region Public methods (will be transformed to commands by ViewModelSource)

        [GenerateButton(Order = -2, BindCommandTo = "GoBackCommand", BindTextTo = nameof(GoBackLabel))]
        public void GoBack() => HostService.GoBack();

        public bool CanGoBack() => HostService?.CanGoBack() ?? false;

        [GenerateButton(Order = -1, BindCommandTo = "GoForwardCommand", BindTextTo = nameof(GoForwardLabel))]
        public void GoForward() => HostService.GoForward();

        public bool CanGoForward() => HostService?.CanGoForward() ?? false;

        public void OpenEMApiDonationUrl()
        {
            if (string.IsNullOrWhiteSpace(EMApiDonationUrl))
                return;

            Process.Start(EMApiDonationUrl);
        }

        #endregion
    }
}