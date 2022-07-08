using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication11.Models
{
    
        public class User
        {
            public int Id { get; set; }
            public string FName { get; set; }
            public string LName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public DateTime DOB { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string Mobile { get; set; }
            public List<Newpost> Newpost { get; set; }
            public string Gender { get; set; }

        [NotMapped]
        public IFormFile postImg1 { get; set; }

        public string postImg { get; set; }

        public List<Friends> friends { get; set; }
    }
}

