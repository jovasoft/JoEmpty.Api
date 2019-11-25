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
            List<Client> clients = clientService.GetList();

            if (clients == null || clients.Count == 0 ) return NotFound();

            List<ClientModel> clientModels = new List<ClientModel>();

            clients.ForEach(x => { clientModels.Add(ClientModel.DtoToModel(x)); });

            return Ok(clientModels);
        }

        // GET: api/Clients/id/5
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            Client client = clientService.Get(id);

            if (client == null) return NotFound();

            return Ok(ClientModel.DtoToModel(client));
        }

        // POST: api/Clients
        [HttpPost]
        public IActionResult Post([FromBody] ClientModel clientModel)
        {
            if (!string.IsNullOrEmpty(clientModel.CurrentCode))
            {
                Client isExists = clientService.Get(clientModel.CurrentCode);

                if (isExists != null) return BadRequest();
            }

            Client client = ClientModel.ModelToDto(clientModel);
            clientService.Add(client);

            ClientModel created = ClientModel.DtoToModel(client);

            return CreatedAtAction(nameof(GetOne), new { created.Id }, created);
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ClientModel clientModel)
        {
            if (id == Guid.Empty) return NotFound();

            Client client = clientService.Get(id);

            if (client == null) return NotFound();

            if (!string.IsNullOrEmpty(clientModel.CurrentCode))
            {
                Client isExists = clientService.Get(clientModel.CurrentCode);

                if (isExists != null && id != isExists.Id) return BadRequest();

                client.CurrentCode = clientModel.CurrentCode;
            }
            if (!string.IsNullOrEmpty(clientModel.Address)) client.Address = clientModel.Address;
            if (!string.IsNullOrEmpty(clientModel.Note)) client.Note = clientModel.Note;
            if (!string.IsNullOrEmpty(clientModel.Title)) client.Title = clientModel.Title;
               
            clientService.Update(client);

            ClientModel accepted = ClientModel.DtoToModel(client);

            return Accepted(accepted);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            Client client = clientService.Get(id);

            if (client == null) return NotFound();

            clientService.Delete(id);

            return NoContent();
        }
    }
}