using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using WearMatcher.Data;
using WearMatcher.Models;
using WearMatcher.ViewModels.api;

namespace WearMatcher.Controllers
{
    [ApiController]
    [Route("{controller}/{action}/")]
    [EnableCors("ClothingItemsController")]
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
            ClothingItem item =
                await _context
                .ClothingItem
                .Include(i => i.Tags)
                .Include(i => i.MatchingItems)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item != null)
            {
                return new OkObjectResult(new ClothingItemFullViewModel(item));
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string search = "")
        {
            List<ClothingItem> items =
                await _context.ClothingItem
                .Where(i => i.Name.Contains(search))
                .Include(i => i.Tags)
                .Include(i => i.MatchingItems)
                .ToListAsync();

            List<ClothingItemFullViewModel> viewModel = new();

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
        public async Task<IActionResult> Create([FromForm] ClothingItemFullViewModel viewModel, [FromForm] IFormFile clothingImg)
        {
            if (ModelState.IsValid)
            {
                ClothingItem item = await viewModel.Reflect(_context);

                _context.Add(item);
                _context.ChangeTracker.DetectChanges();
                Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", item.Id, new ClothingItemFullViewModel(item));
            }

            return BadRequest();
        }

        // POST: ClothingItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] ClothingItemFullViewModel itemViewModel)
        {
            if (ModelState.IsValid)
            {
                ClothingItem item = await itemViewModel.Reflect(_context);

                _context.Update(item);
                await _context.SaveChangesAsync();

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
    }
}
