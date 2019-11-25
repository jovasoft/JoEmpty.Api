using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class FacilityMaganer : IFacilityService
    {
        IFacilityDal facilityDal;

        public FacilityMaganer(IFacilityDal facilityDal)
        {
            this.facilityDal = facilityDal;
        }

        public void Add(Facility facility)
        {
            facilityDal.Add(facility);
        }

        public void Delete(Guid id)
        {
            facilityDal.Delete(facilityDal.Get(x => x.Id == id));
        }

        public Facility Get(string code)
        {
            return facilityDal.Get(x => x.Code == code);
        }

        public List<Facility> GetList(Guid contractId)
        {
            return facilityDal.GetList(x => x.ContractId == contractId);
        }

        public Facility Get(Guid id)
        {
            return facilityDal.Get(x => x.Id == id);
        }

        public List<Facility> GetList()
        {
            return facilityDal.GetList();
        }

        public void Update(Facility facility)
        {
            facilityDal.Update(facility);
        }
    }
}
