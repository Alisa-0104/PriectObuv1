using System.Windows;
using ShoeStoreApp.Data;

namespace ShoeStoreApp
{
    public partial class LoginWindow : Window
    {
        private readonly DatabaseHelper _dbHelper;

        public LoginWindow()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = _dbHelper.AuthenticateUser(login, password);

            if (user != null)
            {
                MessageBox.Show($"Добро пожаловать, {user.FirstName} {user.LastName}!\nРоль: {user.Role}",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Передаем пользователя в главное окно
                MainWindow mainWindow = new MainWindow(user);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            // Вход как гость (без авторизации)
            MainWindow mainWindow = new MainWindow(null);
            mainWindow.Show();
            this.Close();
        }
    }
}