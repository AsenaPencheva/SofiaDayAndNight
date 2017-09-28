using System.Collections.Generic;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models.Abstracts;

namespace SofiaDayAndNight.Data.Models
{
    public class Multimedia : BaseModel
    {
        //public Multimedia()
        //{
        //    this.Images = new HashSet<Image>(); 
        //}

        public Privacy Privacy { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        //public virtual ICollection<Image> Images { get; set; }

        public virtual Event Event { get; set; }
    }
}
