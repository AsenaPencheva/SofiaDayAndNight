using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SofiaDayAndNight.Data.Models
{
    public class Individual
    {
        public int Id { get; set; }

        public int ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        public virtual ICollection<Individual> Friends { get; set; }

        public virtual ICollection<Place> Following { get; set; }

        public virtual ICollection<Event> EventsAttended { get; set; }

    }
}
