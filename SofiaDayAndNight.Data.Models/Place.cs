using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using SofiaDayAndNight.Data.Models.Abstracts;

namespace SofiaDayAndNight.Data.Models
{
    public class Place : BaseModel
    {
        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        //public int AgeRestriction { get; set; }

        public virtual ICollection<Individual> Followers { get; set; }
    }
}
