using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

using SofiaDayAndNight.Data.Models.Contracts;

namespace SofiaDayAndNight.Data.Models
{
    public class User : IdentityUser, IAuditable, IDeletable, IForbidabble
    {
        public User()
        {
            this.City = "Sofia";
        }

        [MaxLength(100)]
        public string City { get; set; }

        [Index]
        public bool IsForbidden { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [Index]
        public bool IsCompleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public virtual Individual Individual { get; set; }

        public virtual Organization Organization { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
