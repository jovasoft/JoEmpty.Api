﻿using System;
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

        // GET: api/Clients
        [HttpGet]
        public IActionResult Get()
        {
            List<Client> clients = clientService.GetList();

            if (clients == null || clients.Count == 0 ) return Errors("Müşteri listesi bulunamadı.", null, 404);

            List<ClientModel> clientModels = new List<ClientModel>();

            clients.ForEach(x => { clientModels.Add(ClientModel.DtoToModel(x)); });

            return Success(null, clientModels, 200);
        }

        // GET: api/Clients/id/5
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne(Guid id)
        {
            if (id == Guid.Empty) return Errors("Böyle bir ID numaralı müşteri yok.", null, 404);

            Client client = clientService.Get(id);

            if (client == null) return Errors("Böyle bir ID numaralı müşteri yok.", null, 404);

            return Success(null, ClientModel.DtoToModel(client));
        }

        // POST: api/Clients
        [HttpPost]
        public IActionResult Post([FromBody] ClientModel clientModel)
        {
            if (!string.IsNullOrEmpty(clientModel.CurrentCode))
            {
                Client isExists = clientService.Get(clientModel.CurrentCode);

                if (isExists != null) Errors("Bu cari koda tanımlı müşteri vardır.", null);
            }

            Client client = ClientModel.ModelToDto(clientModel);
            clientService.Add(client);

            if (clientService.Get(client.Id) == null) return Errors("Müşteri kaydı yapılamadı.", null, 404);

            return Success(null, ClientModel.DtoToModel(client), 201);
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ClientModel clientModel)
        {
            if (id == Guid.Empty) return Errors("Müşteri ID boş olamaz.", null, 404);

            Client client = clientService.Get(id);

            if (client == null) return Errors("Güncellenmek istenen kayıt yoktur.", null, 404);

            if (!string.IsNullOrEmpty(clientModel.CurrentCode))
            {
                Client isExists = clientService.Get(clientModel.CurrentCode);

                if (isExists != null && id != isExists.Id) return Errors("Güncellenmek istenen cari kodu başka müşteriye atanmıştır.", null);

                client.CurrentCode = clientModel.CurrentCode;
            }
            if (!string.IsNullOrEmpty(clientModel.Address)) client.Address = clientModel.Address;
            if (!string.IsNullOrEmpty(clientModel.Note)) client.Note = clientModel.Note;
            if (!string.IsNullOrEmpty(clientModel.Title)) client.Title = clientModel.Title;
               
            clientService.Update(client);

            return Success(null, ClientModel.DtoToModel(client), 202);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return Errors("Böyle bir ID numaralı müşteri yok.", null, 404);

            Client client = clientService.Get(id);

            if (client == null) return Errors("Böyle bir ID numaralı müşteri yok.", null, 404);

            clientService.Delete(id);

            return Success(null, null, 204);
        }
    }
}