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

        public ClothingItemViewModel() {}

        public ClothingItemViewModel(ClothingItem item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.ImgPath = item.ImgPath;
        }

        public ClothingItem Reflect()
        {
            ClothingItem item = new();

            item.Id = this.Id;
            item.Name = this.Name;
            item.ImgPath = this.ImgPath;

            return item;
        }
    }
}
