using System.ComponentModel.DataAnnotations;

namespace WebApplication11.viewModel
{
    public class CommentViewModel
    {
        [Required]
        public int postId { get; set; }
        [Required]
        public string text { get; set; }
    }
}
