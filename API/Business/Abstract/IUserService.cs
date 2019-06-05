using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        User Get(string mail);

        void Update(User user);

        bool ChangePassword(User user, string oldPassword, string newPassword);

        void Delete(User user);
    }
}
