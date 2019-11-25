using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IClientContactService
    {
        void Add(ClientContact clientContact);

        ClientContact Get(Guid id);

        void Delete(Guid id);

        void Update(ClientContact clientContact);

        List<ClientContact> GetList(Guid clientId);
    }
}
