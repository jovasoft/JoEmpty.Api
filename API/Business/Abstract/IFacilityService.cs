using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFacilityService
    {
        void Add(Facility facility);

        Facility GetByCode(string code);

        List<Facility> GetByContractId(Guid contractId);

        Facility GetByFacilityId(Guid id);

        void Delete(Guid id);

        bool Update(Facility facility);

        List<Facility> GetList();
    }
}
