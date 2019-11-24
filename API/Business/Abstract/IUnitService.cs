using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUnitService
    {
        void Add(Unit unit);

        Unit GetByCode(string code);

        List<Unit> GetByContractId(Guid contractId);

        Unit GetByUnitId(Guid id);

        void Delete(Guid id);

        bool Update(Unit unit);

        List<Unit> GetList();
    }
}
