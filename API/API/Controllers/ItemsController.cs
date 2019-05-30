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
        private IUserService userService;

        public ItemsController(IItemService itemService, IUserService userService)
        {
            this.itemService = itemService;
            this.userService = userService;
        }

        // GET: api/Items
        [HttpGet]
        public IActionResult Get()
        {
            User user = userService.Get(User.Identity.Name);
            List<ItemModel> itemModels = new List<ItemModel>();
            ItemModel itemModel = new ItemModel();
            List<Item> userItems = itemService.GetList(user.Id);

            foreach (var userItem in userItems)
            {
                itemModel.Id = userItem.Id;
                itemModel.Name = userItem.Name;
                itemModel.Desc = userItem.Desc;
                itemModel.CreateDate = userItem.CreateDate;

                itemModels.Add(itemModel);
            }

            return Ok(itemModels);
        }

        // GET: api/Items/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                Item item = itemService.Get(id);
                ItemModel itemModel = new ItemModel { Name = item.Name , Desc = item.Desc, CreateDate = item.CreateDate, Id = item.Id};

                return Ok(itemModel);
            }

            return BadRequest();
        }

        // POST: api/Items
        [HttpPost]
        public IActionResult Post([FromBody] ItemModel itemModel)
        {

            if (ModelState.IsValid)
            {
                User user = userService.Get(User.Identity.Name);

                Item item = new Item { Name = itemModel.Name, Desc = itemModel.Desc, CreateDate = itemModel.CreateDate, UserId = user.Id};
                itemService.Add(item);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {
                User user = userService.Get(User.Identity.Name);

                Item item = itemService.Get(user.Id);
                item.Name = itemModel.Name;
                item.UserId = user.Id;
                item.Desc = itemModel.Desc;
                item.CreateDate = itemModel.CreateDate;
                itemService.Update(item);

                return Ok(new { status = "success" });
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
                return Ok(new { status = "success" });
            }

            return BadRequest();
        }
    }
}
