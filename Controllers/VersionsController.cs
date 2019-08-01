using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendClassLibrary.Data;
using BackendClassLibrary.Models;
using Version = BackendClassLibrary.Models.Version;

namespace backendcapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VersionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Versions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Version>>> GetVersions()
        {
            return await _context.Versions.ToListAsync();
        }

        // GET: api/Versions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Version>> GetVersion(int id)
        {
            var version = await _context.Versions.FindAsync(id);

            if (version == null)
            {
                return NotFound();
            }

            return version;
        }

        // PUT: api/Versions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVersion(int id, Version version)
        {
            if (id != version.VersionId)
            {
                return BadRequest();
            }

            _context.Entry(version).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VersionExists(id))
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

        // POST: api/Versions
        [HttpPost]
        public async Task<ActionResult<Version>> PostVersion(Version version)
        {
            _context.Versions.Add(version);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVersion", new { id = version.VersionId }, version);
        }

        // DELETE: api/Versions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Version>> DeleteVersion(int id)
        {
            var version = await _context.Versions.FindAsync(id);
            if (version == null)
            {
                return NotFound();
            }

            _context.Versions.Remove(version);
            await _context.SaveChangesAsync();

            return version;
        }

        private bool VersionExists(int id)
        {
            return _context.Versions.Any(e => e.VersionId == id);
        }
    }
}
