using Business.Abstract;
using DataAccess.Abstract;
using Entites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ItemManager : IItemService
    {
        IItemDal itemDal;

        public ItemManager(IItemDal itemDal)
        {
            this.itemDal = itemDal;
        }

        public void Delete(int id)
        {
             itemDal.Delete(itemDal.Get(x => x.Id == id));
        }

        public Item Get(int id)
        {
            return itemDal.Get(x => x.Id == id);
        }

        public void Post([FromBody] Item item)
        {
            itemDal.Add(item);
        }

        public bool Put([FromBody] Item item)
        {
            itemDal.Update(item);

            return true;
        }

    }
}
