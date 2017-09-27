using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using SofiaDayAndNight.Data.Models.Abstracts;

namespace SofiaDayAndNight.Data.Models
{
    public class Individual : BaseModel
    {
        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

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

        public virtual ICollection<Individual> Friends { get; set; }

        public virtual ICollection<Place> Following { get; set; }

        public virtual ICollection<Event> EventsAttended { get; set; }

    }
}
