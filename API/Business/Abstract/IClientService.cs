using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IClientService
    {
        void Add(Client client);

        Client Get(Guid id);

        void Delete(Guid id);

        bool Update(Client client);

        List<Client> GetList();
    }
}
