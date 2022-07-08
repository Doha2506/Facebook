using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication11.Models
{
    public class friendRequest
    {
       
        
            public int ID { get; set; }
            public int friendID { get; set; }
            public string status { get; set; }

            // Foreign key   
            [Display(Name = "User")]
            public virtual int UserID { get; set; }

            [ForeignKey("UserID")]
            public virtual User users { get; set; }
        
    }
}
