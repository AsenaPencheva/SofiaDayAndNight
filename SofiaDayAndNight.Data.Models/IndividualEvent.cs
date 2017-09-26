namespace SofiaDayAndNight.Data.Models
{
    public class IndividualEvent
    {       
        public IndividualEvent(int individualId, int eventId) 
        {
            this.IndividualId = individualId;
            this.EventId = eventId;
        }

        public int Id { get; set; }

        public Privacy Privacy { get; set; }

        public int IndividualId { get; set; }

        public int EventId { get; set; }
    }
}
