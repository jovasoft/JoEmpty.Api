using Entites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

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
