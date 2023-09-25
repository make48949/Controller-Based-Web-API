using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Controller_Based_Web_API.Models;

namespace Controller_Based_Web_API.Controllers
{
    [Route("api/[ToDoItems]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoItemsController(ToDoContext context)
        {
            _context = context;
        }

        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            //if (_context.ToDoItems == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.ToDoItems.ToListAsync();
            return await _context.ToDoItems.Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(long id)
        {
            //if (_context.ToDoItems == null)
            //{
            //    return NotFound();
            //}
            //  var toDoItem = await _context.ToDoItems.FindAsync(id);

            //  if (toDoItem == null)
            //  {
            //      return NotFound();
            //  }

            //  return toDoItem;
            var ToDoItem = await _context.ToDoItems.FindAsync(id);

            if(ToDoItem == null) 
            {
                return NotFound();
            }
            return ItemToDTO(ToDoItem);
        }

        // PUT: api/ToDoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(long id, ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return BadRequest();
            }

            // _context.Entry(toDoItem).State = EntityState.Modified;
            toDoItem.Name = toDoDTO.Name;
            toDoItem.IsComplete = toDoDTO.IsComplete;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ToDoItemExists(id))
            {
                {
                    return NotFound();
                }
                
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ToDoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem toDoItem)
        {
          if (_context.ToDoItems == null)
          {
              return Problem("Entity set 'ToDoContext.ToDoItems'  is null.");
          }
            _context.ToDoItems.Add(toDoItem);
            await _context.SaveChangesAsync();

            // Updating the return statement so it can use "nameof" operator.
            return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(long id)
        {
            if (_context.ToDoItems == null)
            {
                return NotFound();
            }
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoItemExists(long id)
        {
            return (_context.ToDoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
