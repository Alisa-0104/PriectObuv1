using System.Windows;

namespace ShoeStoreApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Первым открываем окно входа
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}