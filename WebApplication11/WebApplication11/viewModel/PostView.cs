using System.ComponentModel.DataAnnotations;

namespace WebApplication11.viewModel
{
    public class PostView
    {
        [Required]
        public int userId { get; set; }
    }
}
