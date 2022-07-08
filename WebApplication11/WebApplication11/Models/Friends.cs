namespace WebApplication11.Models
{
    public class Friends
    {
        public int id { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public int friendId { get; set; }
    }
}
