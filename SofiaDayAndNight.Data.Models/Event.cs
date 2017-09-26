using SofiaDayAndNight.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Data.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [DefaultValue(12)]
        public int AgeRestriction { get; set; } 

        public bool IsDeleted { get; set; }

        public bool IsBanned { get; set; } 

        [DefaultValue(0)] //???
        public Privacy Privacy { get; set; }

        [DateRange]
        public DateTime Begins { get; set; }

        public DateTime Ends { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [DefaultValue(10)] //???
        public EventType EventType { get; set; }

        public int MultimediaId { get; set; }

        public virtual Multimedia Multimedia { get; set; }

        public int PlaceId { get; set; }

        public virtual Place Place { get; set; }

        public virtual ICollection<Individual> IndividualsAttended { get; set; }
    }
}
