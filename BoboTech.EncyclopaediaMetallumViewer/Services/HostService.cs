using BoboTech.EncyclopaediaMetallumViewer.Common.Services;
using BoboTech.EncyclopaediaMetallumViewer.Windows;
using DevExpress.Mvvm.UI;
using System;
using System.Windows;

namespace BoboTech.EncyclopaediaMetallumViewer.Services
{
    public class HostService : ServiceBase, IHostService
    {
        private Guid _instanceId = Guid.NewGuid();

        public object DataContext { get => AssociatedObject.DataContext; set => AssociatedObject.DataContext = value; }

        public void Close()
        {
            if (AssociatedObject is Window window)
                window.Close();
        }

        public void ShowInitialView() => new ViewHost().Show();
    }
}