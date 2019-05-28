using Core;
using System;

namespace Entites
{
    public class Item : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
