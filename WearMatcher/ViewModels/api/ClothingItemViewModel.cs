using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WearMatcher.Models;
using WearMatcher.Data;

namespace WearMatcher.ViewModels.api
{
    public class ClothingItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }

        public List<int> Tags { get; set; }
        public List<int> MatchingItems { get; set; }

        public ClothingItemViewModel(ClothingItem item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.ImgPath = item.ImgPath;

            if (item.Tags != null)
                this.Tags = item.Tags.Select(t => t.Id).ToList();

            if (item.MatchingItems != null)
                this.MatchingItems = item.MatchingItems.Select(i => i.Id).ToList();
        }

        public async Task<ClothingItem> Reflect(WearMatcherContext context)
        {
            ClothingItem item = new();

            item.Id = this.Id;
            item.Name = this.Name;
            item.ImgPath = this.ImgPath;

            item.Tags = await context.Tag.Where(t => this.Tags.Contains(t.Id)).ToListAsync();
            item.MatchingItems = await context.ClothingItem.Where(i => this.MatchingItems.Contains(i.Id)).ToListAsync();

            return item;
        }
    }
}
