﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebSite { get; set; }
        public List<Media> Medias { get; set; }

    }
}
