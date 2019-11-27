using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientContactController : ResponseController
    {
        private IClientContactService clientContactService;

        public ClientContactController(IClientContactService clientContactService)
        {
            this.clientContactService = clientContactService;
        }

        // GET: api/ClientsContact/clientId
        [HttpGet("{clientId}")]
        public IActionResult Get(Guid clientId)
        {
            if (clientId == Guid.Empty) return Error("Eşleşen kayıt bulunamadı.", null, 404);

            List<ClientContact> clientsContact = clientContactService.GetList(clientId);

            if (clientsContact == null || clientsContact.Count == 0) return Error("Eşleşen kayıt bulunamadı.", null, 404);

            List<ClientContactModel> clientContactModels = new List<ClientContactModel>();

            clientsContact.ForEach(x => { clientContactModels.Add(ClientContactModel.DtoToModel(x)); } );

            return Success(null, clientContactModels);
        }

        // GET: api/ClientsContact/GetOne/id
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return Error("Yetkili kişi bulunamadı.", null, 404);

            ClientContact clientContact = clientContactService.Get(id);

            if (clientContact == null) return Error("Yetkili kişi bulunamadı.", null, 404);

            return Success(null, ClientContactModel.DtoToModel(clientContact));
        }

        // POST: api/ClientsContact
        [HttpPost]
        public IActionResult Post([FromBody] ClientContactModel clientContactModel)
        {
            if (clientContactModel.ClientId == Guid.Empty) return Error("Müşteri bulunamadı.", null, 404);

            ClientContact clientContact = ClientContactModel.ModelToDto(clientContactModel);
            clientContactService.Add(clientContact);

            if (clientContactService.Get(clientContact.Id) == null) return Error("Yetkili kişi eklenemedi.", null);

            return Success(null, ClientContactModel.DtoToModel(clientContact), 201);
        }

        // PUT: api/ClientsContact/id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ClientContactModel clientContactModel)
        {
            if (id == Guid.Empty) return Error("Yetkili kişi bulunamadı.", null, 404);

            ClientContact clientContact = clientContactService.Get(id);

            if (clientContact == null) Error("Yetkili kişi bulunamadı.", null, 404);

            if (!string.IsNullOrEmpty(clientContactModel.Department)) clientContact.Department = clientContactModel.Department;
            if (!string.IsNullOrEmpty(clientContactModel.FirstName)) clientContact.FirstName = clientContactModel.FirstName;
            if (!string.IsNullOrEmpty(clientContactModel.LastName)) clientContact.LastName = clientContactModel.LastName;
            if (!string.IsNullOrEmpty(clientContactModel.InternalNumber)) clientContact.InternalNumber = clientContactModel.InternalNumber;
            if (!string.IsNullOrEmpty(clientContactModel.MailAddress)) clientContact.MailAddress = clientContactModel.MailAddress;
            if (!string.IsNullOrEmpty(clientContactModel.PhoneNumber)) clientContact.PhoneNumber = clientContactModel.PhoneNumber;
            if (!string.IsNullOrEmpty(clientContactModel.Title)) clientContact.Title = clientContactModel.Title;

            clientContactService.Update(clientContact);

            return Success(null, ClientContactModel.DtoToModel(clientContact), 202);
        }

        // DELETE: api/ClientsContact/id
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return Error("Yetkili kişi bulunamadı.", null, 404);

            ClientContact clientContact = clientContactService.Get(id);

            if (clientContact == null) return Error("Yetkili kişi bulunamadı.", null, 404);

            clientContactService.Delete(id);

            return Success(null, null, 204);
        }
    }
}