﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class AppConfig
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Details1 { get; set; }
        public string Details2 { get; set; }
        public string Details3 { get; set; }

    }
}
