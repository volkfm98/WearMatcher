using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WearMatcher.Models;
using WearMatcher.Data;

namespace WearMatcher.ViewModels.api
{
    public class ClothingItemFullViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }

        public List<int> TagIds { get; set; }
        public List<int> MatchingItemIds { get; set; }

        public ClothingItemFullViewModel() {}

        public ClothingItemFullViewModel(ClothingItem item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.ImgPath = item.ImgPath;

            if (item.Tags != null)
                this.TagIds = item.Tags.Select(t => t.Id).ToList();

            if (item.MatchingItems != null)
                this.MatchingItemIds = item.MatchingItems.Select(i => i.Id).ToList();
        }

        public async Task<ClothingItem> Reflect(WearMatcherContext context)
        {
            ClothingItem item = new();

            item.Id = this.Id;
            item.Name = this.Name;
            item.ImgPath = this.ImgPath;

            item.Tags = await context.Tag.Where(t => this.TagIds.Contains(t.Id)).ToListAsync();
            item.MatchingItems = await context.ClothingItem.Where(i => this.MatchingItemIds.Contains(i.Id)).ToListAsync();

            return item;
        }
    }
}
