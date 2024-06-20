using CLONETRELLOBACK.Data;
using CLONETRELLOBACK.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CLONETRELLOBACK.Controllers
{
    [Route("api/list")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly TaskContext _context;

        public ListController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/list
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Lists>>> GetLists()
        {
            return await _context.Lists.ToListAsync();
        }

        // GET: api/list/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lists>> GetList(int id)
        {
            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }
            return list;
        }

        // PUT: api/list/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutList(int id, Lists list)
        {
            list.Id = id;

            if (id != list.Id)
            {
                return BadRequest();
            }

            _context.Entry(list).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/list
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Lists>> PostList(Lists list)
        {
            _context.Lists.Add(list);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetList", new { id = list.Id }, list);
        }

        // DELETE: api/list/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListExists(int id)
        {
            return _context.Lists.Any(e => e.Id == id);
        }
    }
}