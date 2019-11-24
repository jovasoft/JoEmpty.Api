using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ClientContactManager : IClientContactService
    {
        IClientContactDal clientContactDal;

        public ClientContactManager(IClientContactDal clientContactDal)
        {
            this.clientContactDal = clientContactDal;
        }

        public void Add(ClientContact clientContact)
        {
            clientContactDal.Add(clientContact);
        }

        public void Delete(Guid clientId)
        {
            clientContactDal.Delete(clientContactDal.Get(x => x.ClientId == clientId));
        }

        public ClientContact Get(Guid clientId)
        {
            return clientContactDal.Get(x => x.ClientId == clientId);
        }

        public List<ClientContact> GetList()
        {
            return clientContactDal.GetList();
        }

        public bool Update(ClientContact clientContact)
        {
            clientContactDal.Update(clientContact);

            return true;
        }
    }
}
