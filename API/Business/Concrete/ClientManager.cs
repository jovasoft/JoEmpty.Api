using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ClientManager : IClientService
    {
        IClientDal clientDal;

        public ClientManager(IClientDal clientDal)
        {
            this.clientDal = clientDal;
        }
        public void Add(Client client)
        {
            clientDal.Add(client);
        }

        public void Delete(Guid id)
        {
            clientDal.Delete(clientDal.Get(x => x.Id == id));
        }

        public Client Get(Guid id)
        {
            return clientDal.Get(x => x.Id == id);
        }

        public Client Get(string currentCode)
        {
            return clientDal.Get(x => x.CurrentCode == currentCode);
        }

        public List<Client> GetList()
        {
            return clientDal.GetList();
        }

        public bool Update(Client client)
        {
            clientDal.Update(client);

            return true;
        }
    }
}
