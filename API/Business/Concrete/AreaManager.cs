using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AreaManager : IAreaService
    {
        IAreaDal areaDal;

        public AreaManager(IAreaDal areaDal)
        {
            this.areaDal = areaDal;
        }

        public void Add(Area area)
        {
            areaDal.Add(area);
        }

        public void Delete(Guid id)
        {
            areaDal.Delete(areaDal.Get(x => x.Id == id));
        }

        public Area Get(Guid id)
        {
            return areaDal.Get(x => x.Id == id);
        }

        public Area Get(string code)
        {
            return areaDal.Get(x => x.Code == code);
        }

        public List<Area> GetList()
        {
            return areaDal.GetList();
        }

        public bool Update(Area area)
        {
            areaDal.Update(area);

            return true;
        }
    }
}
