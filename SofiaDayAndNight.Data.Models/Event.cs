﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using SofiaDayAndNight.Common.Attributes;
using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models.Abstracts;
using SofiaDayAndNight.Data.Models.Contracts;

namespace SofiaDayAndNight.Data.Models
{
    public class Event : BaseModel, IForbidabble
    {
        public Event()
        {
            this.EventType = EventType.Other;
            this.Privacy = Privacy.OnlyFriends;
            this.IndividualsAttended = new HashSet<Individual>();
            this.AccessType = AccessType.None;
        }

        [MinLength(3)]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]       
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        //[DefaultValue(12)]
        //public int AgeRestriction { get; set; }

        public Privacy Privacy { get; set; }

        public AccessType AccessType { get; set; }

        [DateRange]
        public DateTime Begins { get; set; }

        [DateRange]
        public DateTime Ends { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public EventType EventType { get; set; }

        public virtual Image Cover { get; set; }

        public virtual Multimedia Multimedia { get; set; }

        public virtual Individual Individual { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual ICollection<Individual> IndividualsAttended { get; set; }
    }
}
