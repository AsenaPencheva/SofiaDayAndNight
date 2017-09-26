using SofiaDayAndNight.Data.Models.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Data.Models
{
    public class Place : BaseModel
    {
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        //public int AgeRestriction { get; set; }

        public virtual ICollection<Individual> Followers { get; set; }
    }
}
