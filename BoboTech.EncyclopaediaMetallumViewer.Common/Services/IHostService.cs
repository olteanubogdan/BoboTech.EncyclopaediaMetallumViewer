namespace BoboTech.EncyclopaediaMetallumViewer.Common.Services
{
    public interface IHostService
    {
        object DataContext { get; set; }

        void Close();

        void ShowInitialView();
    }
}