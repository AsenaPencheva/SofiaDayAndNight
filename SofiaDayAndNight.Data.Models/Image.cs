using SofiaDayAndNight.Data.Models.Abstracts;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Data.Models
{
    public class Image : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]

        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }

        //public bool IsSelected { get; set; }

        [DefaultValue(Privacy.OnlyFriends)]
        public Privacy Privacy { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public int MultimediaId { get; set; }

        public virtual Multimedia Multimedia { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
