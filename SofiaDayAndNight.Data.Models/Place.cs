using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Data.Models
{
    public class Place
    {
        public int Id { get; set; }

        public int ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        //public int AgeRestriction { get; set; }

        public virtual ICollection<Individual> Followers { get; set; }
    }
}
