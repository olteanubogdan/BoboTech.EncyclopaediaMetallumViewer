namespace BoboTech.EncyclopaediaMetallumViewer.Common.Services
{
    public interface IHostService
    {
        void Close();

        void ShowInitialView();

        void ShowView(object viewModel);

        void GoBack();

        bool CanGoBack();

        void GoForward();

        bool CanGoForward();
    }
}