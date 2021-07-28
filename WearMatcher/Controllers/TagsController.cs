using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMatcher.Data;
using WearMatcher.Models;

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
            return new OkObjectResult(tags);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
             Tag tag = await _context.Tag.FindAsync(id);

            if (tag != null)
            {
                return new OkObjectResult(tag);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm, Bind("Id, Name, ClothingItems")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _context.Tag.AddAsync(tag);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", tag.Id, tag);
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

        public async Task<IActionResult> Update([FromRoute] int id, [FromForm, Bind("Id, Name")] Tag tag)
        {
            if (!_context.Tag.Any(t => t.Id == tag.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tag.Any(t => t.Id == tag.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return new OkObjectResult("Tag was successfully updated");
            }
            return BadRequest();
        }
    }
}
