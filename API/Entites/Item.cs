﻿using Core;
using System;

namespace Entities
{
    public class Item : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
