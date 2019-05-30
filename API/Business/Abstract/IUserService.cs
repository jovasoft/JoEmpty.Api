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

        void Delete(User user);
    }
}
