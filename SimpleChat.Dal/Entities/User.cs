﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleChat.Dal.Entities
{
    class User
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
    }
}
