﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WearMatcher.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ClothingItem> ClothingItems { get; set; }
    }
}
