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

        public void Add(Item item)
        {
            itemDal.Add(item);
        }

        public void Delete(Guid id)
        {
             itemDal.Delete(itemDal.Get(x => x.Id== id));
        }

        public Item Get(Guid id)
        {
            return itemDal.Get(x => x.Id == id);
        }

        public List<Item> GetList(Guid userId)
        {
            return itemDal.GetList(x => x.UserId == userId);
        }

        public List<Item> GetList()
        {
            return itemDal.GetList();
        }

        public bool Update(Item item)
        {
            itemDal.Update(item);

            return true;
        }
    }
}
