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
        IPersonalDal personelDal;

        public PersonalManager(IPersonalDal personelDal)
        {
            this.personelDal = personelDal;
        }

        public void Add(Personal personal)
        {
            personelDal.Add(personal);
        }

        public void Delete(Guid id)
        {
            personelDal.Delete(personelDal.Get(x => x.Id == id));
        }

        public List<Personal> GetList()
        {
            return personelDal.GetList();
        }
    }
}
