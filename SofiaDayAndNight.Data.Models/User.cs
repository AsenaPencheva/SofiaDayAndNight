using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

using SofiaDayAndNight.Data.Models.Contracts;

namespace SofiaDayAndNight.Data.Models
{
    public class User : IdentityUser, IDeletable, IAuditable, IForbidabble
    {
        [MaxLength(100)]
        [DefaultValue("Sofia")]
        public string City { get; set; }

        public bool IsForbidden { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public int ImageId { get; set; }

        public virtual Image ProfileImage { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        //public UserType UserType { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
