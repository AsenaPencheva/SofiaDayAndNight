using System;
using System.Linq;

using Bytes2you.Validation;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Data.Services
{
    public class UserService
    {
        private readonly IEfDbSetWrapper<User> usersSetWrapper;

        private readonly ISaveContext dbContext;

        public UserService(IEfDbSetWrapper<User> usersSetWrapper)
        {
            Guard.WhenArgument(usersSetWrapper, "UsersSetWrapper").IsNull().Throw();

            this.usersSetWrapper = usersSetWrapper;
        }

        public User GetById(Guid id) 
        {
            var user = this.usersSetWrapper.GetById(id);
            return user;
        }

        public User GetByUsername(string username) 
        {
            var user = this.usersSetWrapper.All.FirstOrDefault(u => u.UserName == username);
            return user;
        }

        public void Ban(Guid id)
        {
            var user = this.GetById(id);
            user.IsForbidden = true;
            this.Update(user);
        }

        public void Unban(Guid id)
        {
            var user = this.GetById(id);
            user.IsForbidden = false;
            this.Update(user);
        }

        private void Update(User user)
        {
            this.usersSetWrapper.Update(user);
            this.dbContext.SaveChanges();
        }
    }
}