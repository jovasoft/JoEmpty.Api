using Business.Abstract;
using DataAccess.Abstract;
using Entities;
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

        public void Delete(Guid id)
        {
             itemDal.Delete(itemDal.Get(x => x.Id == id));
        }

        public Item Get(Guid id)
        {
            return itemDal.Get(x => x.Id == id);
        }

        public void Post(Item item)
        {
            itemDal.Add(item);
        }

        public bool Put(Item item)
        {
            itemDal.Update(item);

            return true;
        }

    }
}
