﻿using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Personal : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
