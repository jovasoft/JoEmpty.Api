using Entities;
using System;

namespace Business.Abstract
{
    public interface IItemService
    {
        void Post(Item item);
        Item Get(Guid id);
        void Delete(Guid id);
        bool Put(Item item);

    }
}
