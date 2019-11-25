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

        public void Delete(Guid id)
        {
            clientContactDal.Delete(clientContactDal.Get(x => x.Id == id));
        }

        public ClientContact Get(Guid id)
        {
            return clientContactDal.Get(x => x.Id == id);
        }

        public List<ClientContact> GetList(Guid clientId)
        {
            return clientContactDal.GetList(x => x.ClientId == clientId);
        }

        public void Update(ClientContact clientContact)
        {
            clientContactDal.Update(clientContact);
        }
    }
}
