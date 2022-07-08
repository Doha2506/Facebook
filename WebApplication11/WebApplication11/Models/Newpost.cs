using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication11.Models
{
    public class Newpost
    {
        public int id { get; set; }
        [NotMapped]
        public IFormFile postImg1 { get; set; }

        public string? postImg { get; set; }

        public string postdesc { get; set; }

        public int Status { get; set; }
        public int? like { get; set; }

        public int? comment { get; set; }

        public DateTime DOB { get; set; }
        public int userId { get; set; }
        public User user { get; set; }

        public List<Comment> comments { get; set; }

    }
}
