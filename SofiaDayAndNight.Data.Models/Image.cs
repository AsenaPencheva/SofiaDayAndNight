using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using SofiaDayAndNight.Data.Models.Abstracts;
using SofiaDayAndNight.Common.Enums;
using System;

namespace SofiaDayAndNight.Data.Models
{
    public class Image : BaseModel
    {
        public Image()
        {
            this.Comments = new HashSet<Comment>();
            this.Privacy = Privacy.OnlyFriends;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }

        //public bool IsSelected { get; set; }

        public Privacy Privacy { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public Event Event { get; set; }

        public virtual Individual Individual { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual ICollection<Multimedia> Multimedias { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
