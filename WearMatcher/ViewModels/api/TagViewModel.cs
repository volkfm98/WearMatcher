using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMatcher.Models;

namespace WearMatcher.ViewModels.api
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TagViewModel() {}

        public TagViewModel(Tag tag)
        {
            this.Id = tag.Id;
            this.Name = tag.Name;
        }

        public Tag Reflect()
        {
            Tag tag = new();

            tag.Id = this.Id;
            tag.Name = this.Name;

            return tag;
        }
    }
}
