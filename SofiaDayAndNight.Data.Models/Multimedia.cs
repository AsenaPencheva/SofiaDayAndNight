namespace SofiaDayAndNight.Data.Models
{
    public class Multimedia
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public Privacy Privacy { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
