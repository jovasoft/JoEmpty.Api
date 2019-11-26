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
    public class ClientContactController : ControllerBase
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
            if (clientId == Guid.Empty) return NotFound();

            List<ClientContact> clientsContact = clientContactService.GetList(clientId);

            if (clientsContact == null || clientsContact.Count == 0) return NotFound();

            List<ClientContactModel> clientContactModels = new List<ClientContactModel>();

            clientsContact.ForEach(x => { clientContactModels.Add(ClientContactModel.DtoToModel(x)); } );

            return Ok(clientContactModels);
        }

        // GET: api/ClientsContact/GetOne/id
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            ClientContact clientContact = clientContactService.Get(id);

            if (clientContact == null) return NotFound();

            return Ok(ClientContactModel.DtoToModel(clientContact));
        }

        // POST: api/ClientsContact
        [HttpPost]
        public IActionResult Post([FromBody] ClientContactModel clientContactModel)
        {
            if (clientContactModel.ClientId == Guid.Empty) return NotFound();

            ClientContact clientContact = ClientContactModel.ModelToDto(clientContactModel);
            clientContactService.Add(clientContact);

            ClientContactModel created = ClientContactModel.DtoToModel(clientContact);

            return CreatedAtAction(nameof(GetOne), new { created.Id }, created);
        }

        // PUT: api/ClientsContact/id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ClientContactModel clientContactModel)
        {
            if (id == Guid.Empty) return NotFound();

            ClientContact clientContact = clientContactService.Get(id);

            if (clientContact == null) return NotFound();

            if (!string.IsNullOrEmpty(clientContactModel.Department)) clientContact.Department = clientContactModel.Department;
            if (!string.IsNullOrEmpty(clientContactModel.FirstName)) clientContact.FirstName = clientContactModel.FirstName;
            if (!string.IsNullOrEmpty(clientContactModel.LastName)) clientContact.LastName = clientContactModel.LastName;
            if (!string.IsNullOrEmpty(clientContactModel.InternalNumber)) clientContact.InternalNumber = clientContactModel.InternalNumber;
            if (!string.IsNullOrEmpty(clientContactModel.MailAddress)) clientContact.MailAddress = clientContactModel.MailAddress;
            if (!string.IsNullOrEmpty(clientContactModel.PhoneNumber)) clientContact.PhoneNumber = clientContactModel.PhoneNumber;
            if (!string.IsNullOrEmpty(clientContactModel.Title)) clientContact.Title = clientContactModel.Title;

            clientContactService.Update(clientContact);

            ClientContactModel accepted = ClientContactModel.DtoToModel(clientContact);

            return Accepted(accepted);
        }

        // DELETE: api/ClientsContact/id
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            ClientContact clientContact = clientContactService.Get(id);

            if (clientContact == null) return NotFound();

            clientContactService.Delete(id);

            return NoContent();
        }
    }
}