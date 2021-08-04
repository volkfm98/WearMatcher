using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WearMatcher.Models
{
    public class ClothingItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }

        public List<Tag> Tags { get; set; }
     
        public List<ClothingItem> MatchingItems { get; set; }
        public List<ClothingItem> MatchingItemsReversed { get; set; }

        public List<MatchingItemsPair> ItemItem { get; set; }
        public List<MatchingItemsPair> ItemItemReversed { get; set; }
    }
}
