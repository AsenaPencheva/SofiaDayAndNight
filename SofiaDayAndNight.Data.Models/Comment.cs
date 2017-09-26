using SofiaDayAndNight.Data.Models.Abstracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Data.Models
{
    public class Comment:BaseModel
    {
        [Required]
        public int UserId { get; set; }

        public virtual User Author { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int ImageId { get; set; }

        public virtual Image Image { get; set; }
    }
}
