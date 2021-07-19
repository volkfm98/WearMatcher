using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WearMatcher.Models
{
    public class ClothingItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public List<MatchingItems> MatchingItems { get; set; }
    }

    public class MatchingItems
    {
        public int ItemId { get; set; }
        public virtual ClothingItem Item { get; set; }

        public int MatchingItemId { get; set; }
        public virtual ClothingItem MatchingItem { get; set; }
    }
}
