using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using SofiaDayAndNight.Data.Models.Contracts;

namespace SofiaDayAndNight.Data.Models.Abstracts
{
    public abstract class BaseModel : IDeletable, IAuditable
    {
        public BaseModel()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Index]
        public bool IsDeleted { get ; set ; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get ; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
