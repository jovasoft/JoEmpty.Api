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
    public class ClientsController : ControllerBase
    {
        private IClientService clientService;

        public ClientsController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        // GET: api/Clients
        [HttpGet]
        public IActionResult Get()
        {
            List<ClientModel> clientModels = new List<ClientModel>();
            List<Client> clients = clientService.GetList();

            foreach (var client in clients)
            {
                ClientModel clientModel = new ClientModel();
                clientModel.Id = client.Id;
                clientModel.Note = client.Note;
                clientModel.Title = client.Title;
                clientModel.Address = client.Address;
                clientModel.CurrentCode = client.CurrentCode;

                clientModels.Add(clientModel);
            }

            return Ok(clientModels);
        }

        // GET: api/Clients/id/5
        [HttpGet("id/{id}")]
        public IActionResult Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                Client client = clientService.Get(id);
                ClientModel clientModel = new ClientModel { Title = client.Title, Address = client.Address, Note = client.Note, CurrentCode = client.CurrentCode, Id = client.Id };

                return Ok(clientModel);
            }

            return BadRequest();
        }

        // GET: api/Clients/currentCode/5
        [HttpGet("currentCode/{currentCode}")]
        public IActionResult Get(string currentCode)
        {
            if (currentCode != string.Empty)
            {
                Client client = clientService.Get(currentCode);
                Client clientModel = new Client { Title = client.Title, Address = client.Address, Note = client.Note, CurrentCode = client.CurrentCode, Id = client.Id };

                return Ok(clientModel);
            }

            return BadRequest();
        }

        // POST: api/Clients                    
        [HttpPost]
        public IActionResult Post([FromBody] ClientModel clientModel)
        {

            if (ModelState.IsValid)
            {
               
                Client client = new Client { Title = clientModel.Title, Address = clientModel.Address, Note = clientModel.Note, CurrentCode = clientModel.CurrentCode };
                clientService.Add(client);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // PUT: api/Clients/5
        [HttpPut("{currentCode}")]
        public IActionResult Put([FromBody] ClientModel clientModel)
        {
            if (ModelState.IsValid)
            {
                
                Client client = clientService.Get(clientModel.CurrentCode);
                client.CurrentCode = clientModel.CurrentCode;
                client.Address = clientModel.Address;
                client.Note = clientModel.Note;
                client.Title = clientModel.Title;
                client.Id = client.Id;
                clientService.Update(client);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // DELETE: api/Clients/5
        [HttpDelete("{currentCode}")]
        public IActionResult Delete(string currentCode)
        {
            if (currentCode != string.Empty)
            {
                clientService.Delete(currentCode);
                return Ok(new { status = "success" });
            }

            return BadRequest();
        }
    }
}