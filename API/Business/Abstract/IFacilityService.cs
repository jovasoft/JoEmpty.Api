using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFacilityService
    {
        void Add(Facility facility);

        Facility Get(string code);

        List<Facility> GetList(Guid contractId);

        List<Facility> GetFacilitiesByClient(Guid clientId);

        Facility Get(Guid id);

        void Delete(Guid id);

        void Update(Facility facility);

        List<Facility> GetList();
    }
}
