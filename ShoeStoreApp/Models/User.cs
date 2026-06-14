namespace ShoeStoreApp.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Role { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}