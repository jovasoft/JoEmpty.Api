using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAreaService
    {
        void Add(Area area);

        void Delete(Guid id);

        Area Get(Guid id);

        Area Get(string code);

        bool Update(Area area);

        List<Area> GetList();
    }
}
