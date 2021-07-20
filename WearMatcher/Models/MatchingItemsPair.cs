using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WearMatcher.Models
{
    public class MatchingItemsPair
    {
        public int FirstItemId { get; set; }
        public ClothingItem FirstItem { get; set; }

        public int SecondItemId { get; set; }
        public ClothingItem SecondItem { get; set; }
    }
}
