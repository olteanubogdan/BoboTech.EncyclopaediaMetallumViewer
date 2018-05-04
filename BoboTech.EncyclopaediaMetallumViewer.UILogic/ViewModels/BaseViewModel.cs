using BoboTech.EncyclopaediaMetallumViewer.Common.Services;
using DevExpress.Mvvm;
using System;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        #region Fields

        Guid _instanceId = Guid.NewGuid();

        #endregion

        #region Properties

        protected IMessageBoxService MessageBoxService => GetService<IMessageBoxService>();

        protected IHostService HostService => GetService<IHostService>();

        public virtual string WindowTitle { get; set; } = "Encyclopaedia Metallum Viewer";

        #endregion
    }
}