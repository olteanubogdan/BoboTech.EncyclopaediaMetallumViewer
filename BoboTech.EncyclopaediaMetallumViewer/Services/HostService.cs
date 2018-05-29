using BoboTech.EncyclopaediaMetallumViewer.Common.Services;
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

        public void ShowView(object viewModel)
        {
            if (viewModel is ISupportParentViewModel supportParent)
                supportParent.ParentViewModel = AssociatedObject.DataContext;

            AssociatedObject.DataContext = viewModel;
        }

        public void ShowViewInNewWindow(object viewModel) => new ViewHost { DataContext = viewModel }.Show();

        public void GoBack()
        {
            if (AssociatedObject.DataContext is ISupportParentViewModel supportParent)
            {
                if (supportParent.ParentViewModel is ISupportChildViewModel supportChild)
                    supportChild.ChildViewModel = AssociatedObject.DataContext;
                AssociatedObject.DataContext = supportParent.ParentViewModel;
            }
        }

        public bool CanGoBack() => AssociatedObject.DataContext is ISupportParentViewModel supportParent ? !(supportParent.ParentViewModel is null) : false;

        public void GoForward()
        {
            if (AssociatedObject.DataContext is ISupportChildViewModel supportChild)
            {
                if (supportChild.ChildViewModel is ISupportParentViewModel supportParent)
                    supportParent.ParentViewModel = AssociatedObject.DataContext;
                AssociatedObject.DataContext = supportChild.ChildViewModel;
            }
        }

        public bool CanGoForward() => AssociatedObject.DataContext is ISupportChildViewModel supportChild ? !(supportChild.ChildViewModel is null) : false;

        public override string ToString() => $"{nameof(HostService)} ({_instanceId:N})";

        #endregion
    }
}