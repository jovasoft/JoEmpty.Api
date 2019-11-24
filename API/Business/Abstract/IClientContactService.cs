using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IClientContactService
    {
        void Add(ClientContact clientContact);

        ClientContact Get(Guid clientId);

        void Delete(Guid clientId);

        bool Update(ClientContact clientContact);

        List<ClientContact> GetList();
    }
}
