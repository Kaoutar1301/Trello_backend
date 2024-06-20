using CLONETRELLOBACK.Data;
using CLONETRELLOBACK.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly TaskContext _context;

    public CommentController(TaskContext context)
    {
        _context = context;
    }

    // GET: api/comment
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<Comments>>> GetComment()
    {
        return await _context.Comments.ToListAsync();
    }

    // GET: api/comment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Comments>> GetComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return comment;
    }

    // PUT: api/comment/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutComment(int id, Comments comment)
    {
        if (id != comment.Id)
        {
            return BadRequest();
        }

        _context.Entry(comment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CommentExists(id))
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

    // POST: api/comment
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Route("")]
    public async Task<ActionResult<Comments>> PostComment(Comments comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
    }

    // DELETE: api/comment/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CommentExists(int id)
    {
        return _context.Comments.Any(e => e.Id == id);
    }
}