using System;
using System.Windows;
using System.Windows.Controls;
using ShoeStoreApp.Data;
using ShoeStoreApp.Models;

namespace ShoeStoreApp
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly User _currentUser;

        public MainWindow(User user)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _currentUser = user;

            // Настройка интерфейса в зависимости от роли
            SetupInterface();

            // Загружаем товары по умолчанию
            LoadTovars();
        }

        private void SetupInterface()
        {
            if (_currentUser != null)
            {
                // Авторизованный пользователь
                UserInfoText.Text = $"{_currentUser.FirstName} {_currentUser.LastName} ({_currentUser.Role})";

                // Показываем меню в зависимости от роли
                if (_currentUser.Role == "Администратор" || _currentUser.Role == "Менеджер")
                {
                    MenuUsers.Visibility = Visibility.Visible;
                    MenuOrders.Visibility = Visibility.Visible;
                    MenuPickup.Visibility = Visibility.Visible;
                }
                else if (_currentUser.Role == "Авторизованный клиент")
                {
                    MenuOrders.Visibility = Visibility.Visible;
                }
            }
            else
            {
                // Гость
                UserInfoText.Text = "Гость";
            }
        }

        private void LoadTovars()
        {
            try
            {
                var tovars = _dbHelper.GetAllTovars();
                TovarsList.ItemsSource = tovars;
                TovarsList.Visibility = Visibility.Visible;
                MainDataGrid.Visibility = Visibility.Collapsed;
                Title = $"Магазин обуви - Товары ({tovars.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowTovars_Click(object sender, RoutedEventArgs e) => LoadTovars();

        private void ShowUsers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var users = _dbHelper.GetAllUsers();
                MainDataGrid.ItemsSource = users;
                MainDataGrid.Visibility = Visibility.Visible;
                TovarsList.Visibility = Visibility.Collapsed;
                Title = $"Пользователи ({users.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var orders = _dbHelper.GetAllOrders();
                MainDataGrid.ItemsSource = orders;
                MainDataGrid.Visibility = Visibility.Visible;
                TovarsList.Visibility = Visibility.Collapsed;
                Title = $"Заказы ({orders.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowPickupPoints_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var points = _dbHelper.GetAllPickupPoints();
                MainDataGrid.ItemsSource = points;
                MainDataGrid.Visibility = Visibility.Visible;
                TovarsList.Visibility = Visibility.Collapsed;
                Title = $"Пункты выдачи ({points.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Возврат к окну входа
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}