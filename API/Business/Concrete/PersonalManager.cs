using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class PersonalManager : IPersonalService
    {
        IPersonalDal personalDal;

        public PersonalManager(IPersonalDal personelDal)
        {
            this.personalDal = personelDal;
        }

        public void Add(Personal personal)
        {
            personalDal.Add(personal);
        }

        public Personal Get(Guid id)
        {
            return personalDal.Get(x => x.Id == id);
        }

        public void Delete(Guid id)
        {
            personalDal.Delete(personalDal.Get(x => x.Id == id));
        }

        public List<Personal> GetList()
        {
            return personalDal.GetList();
        }

        public void Update(Personal personal)
        {
            personalDal.Update(personal);
        }
    }
}
