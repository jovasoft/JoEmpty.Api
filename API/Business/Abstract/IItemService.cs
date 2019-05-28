using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Business.Abstract
{
    public interface IItemService
    {
        void Post([FromBody]Item item);
        Item Get(int id);
        void Delete(int id);
        bool Put([FromBody]Item item);

    }
}
