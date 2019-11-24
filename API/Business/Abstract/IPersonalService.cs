using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IPersonalService
    {
        void Add(Personal personal);

        void Delete(Guid id);

        List<Personal> GetList();
    }
}
