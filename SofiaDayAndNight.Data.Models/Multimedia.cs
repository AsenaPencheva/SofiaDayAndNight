using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models.Abstracts;

namespace SofiaDayAndNight.Data.Models
{
    public class Multimedia : BaseModel
    {
        public Privacy Privacy { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
