using System;
using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public int ApplicationUserId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public bool isDeleted { get; set; }

        [Required]
        public int ImageId { get; set; }

        public virtual Image Image { get; set; }
    }
}
