using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UnitMaganer : IUnitService
    {
        IUnitDal unitDal;

        public UnitMaganer(IUnitDal unitDal)
        {
            this.unitDal = unitDal;
        }

        public void Add(Unit unit)
        {
            unitDal.Add(unit);
        }

        public void Delete(Guid id)
        {
            unitDal.Delete(unitDal.Get(x => x.Id == id));
        }

        public Unit GetByCode(string code)
        {
            return unitDal.Get(x => x.Code == code);
        }

        public List<Unit> GetByContractId(Guid contractId)
        {
            return unitDal.GetList(x => x.ContractId == contractId);
        }

        public Unit GetByUnitId(Guid id)
        {
            return unitDal.Get(x => x.Id == id);
        }

        public List<Unit> GetList()
        {
            return unitDal.GetList();
        }

        public bool Update(Unit unit)
        {
            unitDal.Update(unit);

            return true;
        }
    }
}
