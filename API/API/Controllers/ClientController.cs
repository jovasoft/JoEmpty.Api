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
    public class ClientController : ResponseController
    {
        private IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        // GET: api/Client
        [HttpGet]
        public IActionResult Get()
        {
            List<Client> clients = clientService.GetList();

            if (clients == null || clients.Count == 0 ) return Error("Eşleşen kayıt bulunamadı.", null, 404);

            List<ClientModel> clientModels = new List<ClientModel>();

            clients.ForEach(x => { clientModels.Add(ClientModel.DtoToModel(x)); });

            return Success(null, clientModels);
        }

        // GET: api/Client/id/5
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return Error("Müşteri bulunamadı.", null, 404);

            Client client = clientService.Get(id);

            if (client == null) return Error("Müşteri bulunamadı.", null, 404);

            return Success(null, ClientModel.DtoToModel(client));
        }

        // POST: api/Client
        [HttpPost]
        public IActionResult Post([FromBody] ClientModel clientModel)
        {
            if (!string.IsNullOrEmpty(clientModel.CurrentCode))
            {
                Client isExists = clientService.Get(clientModel.CurrentCode);

                if (isExists != null) Error("Bu cari koda ait bir müşteri zaten var.", null);
            }

            Client client = ClientModel.ModelToDto(clientModel);
            clientService.Add(client);

            if (clientService.Get(client.Id) == null) return Error("Müşteri eklenemedi.", null);

            return Success(null, ClientModel.DtoToModel(client), 201);
        }

        // PUT: api/Client/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ClientModel clientModel)
        {
            if (id == Guid.Empty) return Error("Müşteri bulunamadı.", null, 404);

            Client client = clientService.Get(id);

            if (client == null) return Error("Müşteri bulunamadı.", null, 404);

            if (!string.IsNullOrEmpty(clientModel.CurrentCode))
            {
                Client isExists = clientService.Get(clientModel.CurrentCode);

                if (isExists != null && id != isExists.Id) return Error("Güncellenmek istenen cari kod başka bir müşteriye ait.", null);

                client.CurrentCode = clientModel.CurrentCode;
            }
            if (!string.IsNullOrEmpty(clientModel.Address)) client.Address = clientModel.Address;
            if (!string.IsNullOrEmpty(clientModel.Note)) client.Note = clientModel.Note;
            if (!string.IsNullOrEmpty(clientModel.Title)) client.Title = clientModel.Title;
               
            clientService.Update(client);

            return Success(null, ClientModel.DtoToModel(client), 202);
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return Error("Müşteri bulunamadı.", null, 404);

            Client client = clientService.Get(id);

            if (client == null) return Error("Müşteri bulunamadı.", null, 404);

            clientService.Delete(id);

            return Success(null, null, 204);
        }
    }
}
