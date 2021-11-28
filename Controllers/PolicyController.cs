using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolicyApi.Models;

namespace PolicyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly PolicyContext _context;

        public PolicyController(PolicyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPolicies()
        {
            return await _context.Policies
                .Include(p => p.Party)
                .Include(p => p.Product)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Policy>> GetPolicy(long id)
        {
            var policy = await _context.Policies
                .Include("Party")
                .Include("Product")
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync<Policy>();

            if (policy == default)
            {
                return NotFound();
            }

            return policy;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolicy(long id, PolicyDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var existing = await _context.Policies.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            var updates = dto.ToPolicy();
            existing.Party = updates.Party;
            existing.Product = updates.Product;
            _context.Entry(existing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Policy>> PostPolicy(PolicyDTO dto)
        {
            var policy = dto.ToPolicy();
            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPolicy", new { id = policy.Id }, policy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(long id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }

            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PolicyExists(long id)
        {
            return _context.Policies.Any(e => e.Id == id);
        }
    }
}
