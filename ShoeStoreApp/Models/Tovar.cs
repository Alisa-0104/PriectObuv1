namespace ShoeStoreApp.Models
{
    public class Tovar
    {
        public int ID { get; set; }
        public string Article { get; set; }
        public string ProductName { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal Price { get; set; }
        public string TheSupplier { get; set; }
        public string Manufacturer { get; set; }
        public string ProductCategory { get; set; }
        public int CurrentDiscount { get; set; }
        public int QuantityInStock { get; set; }
        public string ProductDescription { get; set; }

        // Путь к изображению (вычисляется автоматически)
        public string ImagePath
        {
            get
            {
                // Пытаемся найти картинку по ID (1.jpg, 2.jpg и т.д.)
                string imagePath = $"/Images/{ID}.jpg";

                // Если картинки нет, используем заглушку
                // Можно проверить существование файла, но для простоты оставим так
                return imagePath;
            }
        }
    }
}