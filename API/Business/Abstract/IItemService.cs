using Entities;
using System;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IItemService
    {
        void Add(Item item);

        Item Get(Guid id);

        void Delete(Guid id);

        bool Update(Item item);

        List<Item> GetList(Guid userId);

    }
}
