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
    public class ItemsController : ControllerBase
    {
        private IItemService itemService;

        public ItemsController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        // GET: api/Items
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(/*itemService.GetList()*/);
        }

        // GET: api/Items/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                Item item = itemService.Get(id);

                return Ok(item);
            }

            return BadRequest();
        }

        // POST: api/Items
        [HttpPost]
        public IActionResult Post([FromBody] ItemModel itemModel)
        {

            if (ModelState.IsValid)
            {
                Item item = new Item { Name = itemModel.Name, Desc = itemModel.Desc, CreateDate = itemModel.CreateDate };
                itemService.Add(item);

                return Ok(itemModel);
            }

            return BadRequest();
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {
                //User User = userService.Get(User.Identity.Name);

                //item = itemService.Get(id);
                //item.Name = itemModel.Name;
                //item.UserId = itemModel.UserId;
                //item.Desc = itemModel.Desc;
                //item.CreateDate = itemModel.CreateDate;

                //return Ok(itemService.Update(item));
            }

            return BadRequest();
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                itemService.Delete(id);
                return Ok();
            }

            return BadRequest();
        }
    }
}
