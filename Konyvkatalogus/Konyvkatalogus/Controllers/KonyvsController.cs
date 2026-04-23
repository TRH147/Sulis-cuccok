using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Konyvkatalogus.Data;
using Konyvkatalogus.Model;

namespace Konyvkatalogus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KonyvsController : ControllerBase
    {
        private readonly KonyvContext _context;

        public KonyvsController(KonyvContext context)
        {
            _context = context;
        }

        // GET: api/Konyvs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Konyv>>> Getkonyvek()
        {
            return await _context.konyvek.ToListAsync();
        }

        // GET: api/Konyvs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Konyv>> GetKonyv(int id)
        {
            var konyv = await _context.konyvek.FindAsync(id);

            if (konyv == null)
            {
                return NotFound();
            }

            return konyv;
        }

        // PUT: api/Konyvs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKonyv(int id, Konyv konyv)
        {
            if (id != konyv.Id)
            {
                return BadRequest();
            }

            _context.Entry(konyv).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KonyvExists(id))
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

        // POST: api/Konyvs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Konyv>> PostKonyv(Konyv konyv)
        {
            _context.konyvek.Add(konyv);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKonyv", new { id = konyv.Id }, konyv);
        }

        // DELETE: api/Konyvs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKonyv(int id)
        {
            var konyv = await _context.konyvek.FindAsync(id);
            if (konyv == null)
            {
                return NotFound();
            }

            _context.konyvek.Remove(konyv);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KonyvExists(int id)
        {
            return _context.konyvek.Any(e => e.Id == id);
        }
    }
}
