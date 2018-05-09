using BoboTech.EncyclopaediaMetallumViewer.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using System;
using System.Windows;

namespace BoboTech.EncyclopaediaMetallumViewer.Services
{
    public class HostService : ServiceBase, Common.Services.IHostService
    {
        #region Fields

        private Guid _instanceId = Guid.NewGuid();

        #endregion

        #region Public methods

        public void Close()
        {
            if (AssociatedObject is Window window)
                window.Close();
        }

        public void ShowInitialView() => new ViewHost().Show();

        public void ShowView(object viewModel)
        {
            if (viewModel is ISupportParentViewModel supportParent)
                supportParent.ParentViewModel = AssociatedObject.DataContext;

            AssociatedObject.DataContext = viewModel;
        }

        public void GoBack()
        {
            if (AssociatedObject.DataContext is ISupportParentViewModel supportParent)
                AssociatedObject.DataContext = supportParent.ParentViewModel;
        }

        public bool CanGoBack() => AssociatedObject.DataContext is ISupportParentViewModel supportParent ? !(supportParent.ParentViewModel is null) : false;

        public override string ToString() => $"{nameof(HostService)} ({_instanceId:N})";

        #endregion
    }
}