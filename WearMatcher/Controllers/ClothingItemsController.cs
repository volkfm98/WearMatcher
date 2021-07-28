using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WearMatcher.Data;
using WearMatcher.Models;
using WearMatcher.ViewModels.api;

namespace WearMatcher.Controllers
{
    [ApiController]
    [Route("{controller}/{action}/")]
    public class ClothingItemsController : Controller
    {
        private readonly WearMatcherContext _context;

        public ClothingItemsController(WearMatcherContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            ClothingItem item = await _context.ClothingItem.FindAsync(id);

            if (item != null)
            {
                return new OkObjectResult(new ClothingItemViewModel(item));
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string search, [FromQuery] List<int> tagIds)
        {
            if (search == null)
            {
                search = "";
            }

            List<ClothingItem> items =
                await _context.ClothingItem
                .Where(i => i.Name.Contains(search))
                //&& i.Tags.Any(t1 => tagIds.All(t2 => t1.Id == t2))
                .ToListAsync();

            List<ClothingItemViewModel> viewModel = new();

            foreach (var item in items)
            {
                viewModel.Add(new(item));
            }

            return new OkObjectResult(viewModel);
        }

        // POST: ClothingItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        // TODO: Should see how to use it in React with ASP
        public async Task<IActionResult> Create([FromForm, Bind("Name")] ClothingItem clothingItem, [FromForm] IFormFile clothingImg, [FromForm] List<int> itemIds, [FromForm] List<int> tagIds)
        {
            if (ModelState.IsValid)
            {
                if (clothingItem.Tags == null)
                {
                    clothingItem.Tags = new();
                }

                if(clothingItem.MatchingItems == null)
                {
                    clothingItem.MatchingItems = new();
                }

                foreach (var tag in await _context.Tag.Where(t => tagIds.Contains(t.Id)).ToListAsync())
                {
                    clothingItem.Tags.Add(tag);
                }

                foreach (var matchingItem in await _context.ClothingItem.Where(i => itemIds.Contains(i.Id)).ToListAsync())
                {
                    clothingItem.MatchingItems.Add(matchingItem);
                }
                _context.Add(clothingItem);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Get", clothingItem.Id, new ClothingItemViewModel(clothingItem));
            }
            return BadRequest();
        }

        // POST: ClothingItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm, Bind("Id, Name, Tags, MatchingItems")] ClothingItem clothingItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingItemExists(clothingItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return new OkObjectResult("ClothingItem was successfully updated");
            }
            return BadRequest();
        }

        // POST: ClothingItems/Delete/5
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var clothingItem = await _context.ClothingItem.FindAsync(id);

            if (clothingItem != null)
            {
                _context.ClothingItem.Remove(clothingItem);
                await _context.SaveChangesAsync();
                return new OkObjectResult("ClothingItem was successfully deleted");
            }

            return BadRequest();
        }

        private bool ClothingItemExists(int id)
        {
            return _context.ClothingItem.Any(e => e.Id == id);
        }
    }
}
