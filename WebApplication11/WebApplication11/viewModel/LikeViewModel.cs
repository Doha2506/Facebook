using System.ComponentModel.DataAnnotations;

namespace WebApplication11.viewModel
{
    public class LikeViewModel
    {
        [Required]
        public int postId { get; set; }
    }
}
