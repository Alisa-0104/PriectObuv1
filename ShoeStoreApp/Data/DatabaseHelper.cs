using Microsoft.Data.SqlClient;
using ShoeStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace ShoeStoreApp.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ShoeStoreConnection"].ConnectionString;
        }

        // Получить все товары
        public List<Tovar> GetAllTovars()
        {
            var tovars = new List<Tovar>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM dbo.Tovar";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tovars.Add(new Tovar
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Article = reader["Article"]?.ToString(),
                            ProductName = reader["ProductName"]?.ToString(),
                            UnitOfMeasurement = reader["UnitOfMeasurement"]?.ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            TheSupplier = reader["TheSupplier"]?.ToString(),
                            Manufacturer = reader["Manufacturer"]?.ToString(),
                            ProductCategory = reader["ProductCategory"]?.ToString(),
                            CurrentDiscount = Convert.ToInt32(reader["CurrentDiscount"]),
                            QuantityInStock = Convert.ToInt32(reader["QuantityInStock"]),
                            ProductDescription = reader["ProductDescription"]?.ToString()
                        });
                    }
                }
            }

            return tovars;
        }

        // Получить всех пользователей
        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM dbo.[User]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Role = reader["Role"]?.ToString(),
                            LastName = reader["LastName"]?.ToString(),
                            FirstName = reader["FirstName"]?.ToString(),
                            Patronymic = reader["Patronymic"]?.ToString(),
                            Login = reader["Login"]?.ToString(),
                            PasswordHash = reader["PasswordHash"]?.ToString()
                        });
                    }
                }
            }

            return users;
        }

        // Получить все заказы
        // Получить все заказы
        public List<Order> GetAllOrders()
        {
            var orders = new List<Order>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM dbo.Orders";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            OrderNumber = reader["OrderNumber"]?.ToString(),
                            OrderDetails = reader["OrderDetails"]?.ToString(),
                            DateOrder = Convert.ToDateTime(reader["DateOrder"]),
                            DeliveryDate = reader["DeliveryDate"] != DBNull.Value
                                ? (DateTime?)Convert.ToDateTime(reader["DeliveryDate"])
                                : null,
                            PickupPointID = Convert.ToInt32(reader["PickupPointID"]),
                            AuthorizedClientFullName = reader["AuthorizedClientFullName"]?.ToString(),
                            CodeToReceive = Convert.ToInt32(reader["CodeToReceive"]),
                            OrderStatus = reader["OrderStatus"]?.ToString()
                            // UserID удалён - больше не используем
                        });
                    }
                }
            }

            return orders;
        }

        // Получить все пункты выдачи
        public List<PickupPoint> GetAllPickupPoints()
        {
            var points = new List<PickupPoint>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM dbo.PickupPoint";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        points.Add(new PickupPoint
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            PostalCode = reader["PostalCode"]?.ToString(),
                            City = reader["City"]?.ToString(),
                            Street = reader["Street"]?.ToString(),
                            BuildingNumber = reader["BuildingNumber"]?.ToString()
                        });
                    }
                }
            }

            return points;
        }

        // Получить состав заказа (OrderProduct)
        public List<OrderProduct> GetOrderProducts(int orderId)
        {
            var items = new List<OrderProduct>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM dbo.OrderProduct WHERE OrderID = @orderId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new OrderProduct
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                OrderID = Convert.ToInt32(reader["OrderID"]),
                                TovarID = Convert.ToInt32(reader["TovarID"]),
                                Quantity = Convert.ToInt32(reader["Quantity"])
                            });
                        }
                    }
                }
            }

            return items;
        }

        // Авторизация пользователя
        public User AuthenticateUser(string login, string password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM dbo.[User] WHERE Login = @login AND PasswordHash = @password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Role = reader["Role"]?.ToString(),
                                LastName = reader["LastName"]?.ToString(),
                                FirstName = reader["FirstName"]?.ToString(),
                                Patronymic = reader["Patronymic"]?.ToString(),
                                Login = reader["Login"]?.ToString(),
                                PasswordHash = reader["PasswordHash"]?.ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}