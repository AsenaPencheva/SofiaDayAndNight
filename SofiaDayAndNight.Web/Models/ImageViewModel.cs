using System.Collections.Generic;

namespace SofiaDayAndNight.Web.Models
{
    public class ImageViewModel
    {
        public int Id { get; set; }

        //public Privacy Privacy { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public AlbumViewModel Album { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}