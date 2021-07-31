using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMatcher.Data;
using WearMatcher.Models;
using WearMatcher.ViewModels.api;

namespace WearMatcher.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]/")]
    public class TagsController : Controller
    {
        private WearMatcherContext _context;

        public TagsController(WearMatcherContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Tag> tags = await _context.Tag.ToListAsync();

            List<TagViewModel> viewModel = new();

            foreach (var tag in tags)
            {
                viewModel.Add(new(tag));
            }

            return new OkObjectResult(viewModel);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
             Tag tag =
                await _context.Tag
                .Include(t => t.ClothingItems)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tag != null)
            {
                TagFullViewModel viewModel = new(tag);
                return new OkObjectResult(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm/*, Bind("Name, ClothingItemIds")*/] TagFullViewModel tagViewModel)
        {
            Tag tag = await tagViewModel.Reflect(_context);

            if (ModelState.IsValid)
            {
                await _context.Tag.AddAsync(tag);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", tag.Id, tagViewModel);
            }

            return BadRequest();
        }

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Tag tag = await _context.Tag.FindAsync(id);

            if (tag != null)
            {
                _context.Tag.Remove(tag);
                await _context.SaveChangesAsync();

                return new OkObjectResult("Tag was successfully deleted");
            }

            return NotFound();
        }

        public async Task<IActionResult> Update([FromForm] TagFullViewModel tagViewModel)
        {
            if (!_context.Tag.Any(t => t.Id == tagViewModel.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Tag tag = await tagViewModel.Reflect(_context);

                _context.Update(tag);
                await _context.SaveChangesAsync();
                
                return new OkObjectResult("Tag was successfully updated");
            }

            return BadRequest();
        }
    }
}
