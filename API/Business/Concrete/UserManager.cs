using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal userDal;

        public UserManager(IUserDal userDal)
        {
            this.userDal = userDal;
        }

        public User Get(string mail)
        {
            return userDal.Get(x => x.Mail == mail);
        }

        public void Update(User user)
        {
            userDal.Update(user);
        }

        public void Delete(User user)
        {
            userDal.Delete(user);
        }

        public bool ChangePassword(User user, string oldPassword, string newPassword)
        {
            if (user.Password != oldPassword) return false;

            user.Password = newPassword;

            Update(user);

            return true;
        }
    }
}
