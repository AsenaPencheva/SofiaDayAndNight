using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using SofiaDayAndNight.Data.Models.Abstracts;
using System;

namespace SofiaDayAndNight.Data.Models
{
    public class Organization : BaseModel
    {
        public Organization()
        {
            this.Events = new HashSet<Event>();
            this.Followers = new HashSet<Individual>();
        }
        public virtual User User { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        //public int AgeRestriction { get; set; }

        public virtual Image ProfileImage { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Individual> Followers { get; set; }
    }
}
