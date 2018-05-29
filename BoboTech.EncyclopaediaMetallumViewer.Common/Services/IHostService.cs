namespace BoboTech.EncyclopaediaMetallumViewer.Common.Services
{
    public interface IHostService
    {
        void Close();

        void ShowView(object viewModel);

        void ShowViewInNewWindow(object viewModel);

        void GoBack();

        bool CanGoBack();

        void GoForward();

        bool CanGoForward();
    }
}