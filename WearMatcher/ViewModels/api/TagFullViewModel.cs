using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WearMatcher.Models;
using WearMatcher.Data;

namespace WearMatcher.ViewModels.api
{
    public class TagFullViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<int> ClothingItemIds {get;set;}

        public TagFullViewModel() {}

        public TagFullViewModel(Tag tag)
        {
            this.Id = tag.Id;
            this.Name = tag.Name;

            this.ClothingItemIds = 
                tag.ClothingItems
                .Select(i => i.Id)
                .ToList();
        }

        public async Task<Tag> Reflect(WearMatcherContext context)
        {
            Tag tag = new();

            tag.Id = this.Id;
            tag.Name = this.Name;

            tag.ClothingItems = await context.ClothingItem.Where(i => this.ClothingItemIds.Contains(i.Id)).ToListAsync();

            return tag;
        }
    }
}
