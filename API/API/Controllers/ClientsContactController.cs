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
    public class ClientsContactController : ControllerBase
    {
        private IClientContactService clientContactService;

        public ClientsContactController(IClientContactService clientContactService)
        {
            this.clientContactService = clientContactService;
        }

        // GET: api/ClientsContact
        [HttpGet]
        public IActionResult Get()
        {
            List<ClientContactModel> clientContactModels = new List<ClientContactModel>();
            List<ClientContact> clientsContact = clientContactService.GetList();

            foreach (var clientContact in clientsContact)
            {
                ClientContactModel clientContactModel = new ClientContactModel();
                clientContactModel.Id = clientContact.Id;
                clientContactModel.ClientId = clientContact.ClientId;
                clientContactModel.Department = clientContact.Department;
                clientContactModel.FirstName = clientContact.FirstName;
                clientContactModel.LastName = clientContact.LastName;
                clientContactModel.InternalNumber = clientContact.InternalNumber;
                clientContactModel.MailAddress = clientContact.MailAddress;
                clientContactModel.PhoneNumber = clientContact.PhoneNumber;
                clientContactModel.Title = clientContact.Title;
                
                clientContactModels.Add(clientContactModel);
            }

            return Ok(clientContactModels);
        }

        // GET: api/ClientsContact/5
        [HttpGet("{clientId}")]
        public IActionResult Get(Guid clientId)
        {
            if (clientId != Guid.Empty)
            {
                ClientContact clientContact = clientContactService.Get(clientId);
                if (clientContact != null)
                {
                    ClientContactModel clientContactModel = new ClientContactModel { Title = clientContact.Title, Department = clientContact.Department, FirstName = clientContact.FirstName, LastName = clientContact.LastName, Id = clientContact.Id, ClientId = clientContact.ClientId, InternalNumber = clientContact.InternalNumber, MailAddress = clientContact.MailAddress, PhoneNumber = clientContact.PhoneNumber };

                    return Ok(clientContactModel);
                }

                return BadRequest();
            }

            return BadRequest();
        }

        // POST: api/ClientsContact                    
        [HttpPost]
        public IActionResult Post([FromBody] ClientContactModel clientContactModel)
        {

            if (ModelState.IsValid)
            {

                ClientContact clientContact = new ClientContact { Title = clientContactModel.Title, Department = clientContactModel.Department, FirstName = clientContactModel.FirstName, LastName = clientContactModel.LastName, ClientId = clientContactModel.ClientId, InternalNumber = clientContactModel.InternalNumber, MailAddress = clientContactModel.MailAddress, PhoneNumber = clientContactModel.PhoneNumber };
                clientContactService.Add(clientContact);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // PUT: api/ClientsContact/5
        [HttpPut("{clientId}")]
        public IActionResult Put([FromBody] ClientContactModel clientContactModel)
        {
            if (ModelState.IsValid)
            {

                ClientContact clientContact = clientContactService.Get(clientContactModel.ClientId);
                clientContact.Department = clientContactModel.Department;
                clientContact.FirstName = clientContactModel.FirstName;
                clientContact.LastName = clientContactModel.LastName;
                clientContact.InternalNumber = clientContactModel.InternalNumber;
                clientContact.MailAddress = clientContactModel.MailAddress;
                clientContact.PhoneNumber = clientContactModel.PhoneNumber;
                clientContact.Title = clientContactModel.Title;
                clientContact.Id = clientContact.Id;
                clientContact.ClientId = clientContact.ClientId;
                clientContactService.Update(clientContact);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // DELETE: api/ClientsContact/5
        [HttpDelete("{currentId}")]
        public IActionResult Delete(Guid currentId)
        {
            if (currentId != Guid.Empty)
            {
                clientContactService.Delete(currentId);
                return Ok(new { status = "success" });
            }

            return BadRequest();
        }
    }
}