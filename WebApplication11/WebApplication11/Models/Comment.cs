namespace WebApplication11.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string text { get; set; }
        public DateTime created { get; set; }
        public int postId { get; set; }
        public Newpost post { get; set; }

        public string UserName { get; set; }
    }
}
