using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using SofiaDayAndNight.Data.Models.Abstracts;
using System;

namespace SofiaDayAndNight.Data.Models
{
    public class Individual : BaseModel
    {
        public Individual()
        {
            this.Friends = new HashSet<Individual>();
            this.Events = new HashSet<Event>();
            this.EventsAttended = new HashSet<Event>();
            this.Following = new HashSet<Organization>();
            this.Commented = new HashSet<Comment>();
        }
        public virtual User User { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        public Guid ImageId { get; set; }
        public virtual Image ProfileImage { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Individual> Friends { get; set; }

        public virtual ICollection<Individual> FriendRequests { get; set; }

        public virtual ICollection<Organization> Following { get; set; }

        public virtual ICollection<Event> EventsAttended { get; set; }

        public virtual ICollection<Comment> Commented { get; set; }
    }
}
