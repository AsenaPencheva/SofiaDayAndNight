using System.ComponentModel.DataAnnotations;

using SofiaDayAndNight.Data.Models.Abstracts;
using System;

namespace SofiaDayAndNight.Data.Models
{
    public class Comment : BaseModel
    {
        public Guid IndividualId { get; set; }
        public virtual Individual Author { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        public Guid ImageId { get; set; }
        public virtual Image Image { get; set; }
    }
}
