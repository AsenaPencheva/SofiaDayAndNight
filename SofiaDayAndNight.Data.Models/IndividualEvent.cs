using SofiaDayAndNight.Common.Enums;

namespace SofiaDayAndNight.Data.Models
{
    public class IndividualEvent
    {
        public IndividualEvent()
        {
            this.Privacy = Privacy.OnlyFriends;
        }

        public int Id { get; set; }

        public Privacy Privacy { get; set; }

        public int IndividualId { get; set; }

        public int EventId { get; set; }
    }
}
