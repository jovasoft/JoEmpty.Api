using Core;
using System;

namespace Entities
{
    public class Item : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
