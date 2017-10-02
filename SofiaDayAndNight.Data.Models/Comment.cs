using System.ComponentModel.DataAnnotations;

using SofiaDayAndNight.Data.Models.Abstracts;

namespace SofiaDayAndNight.Data.Models
{
    public class Comment : BaseModel
    {
        public virtual User Author { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        public virtual Image Image { get; set; }
    }
}
