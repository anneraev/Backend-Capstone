using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendClassLibrary.Data;
using BackendClassLibrary.Models;

namespace backendcapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserVersionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserVersionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserVersions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserVersion>>> GetUserVersions()
        {
            return await _context.UserVersions.ToListAsync();
        }

        // GET: api/UserVersions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserVersion>> GetUserVersion(int id)
        {
            var userVersion = await _context.UserVersions.FindAsync(id);

            if (userVersion == null)
            {
                return NotFound();
            }

            return userVersion;
        }

        // PUT: api/UserVersions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserVersion(int id, UserVersion userVersion)
        {
            if (id != userVersion.UserVersionId)
            {
                return BadRequest();
            }

            _context.Entry(userVersion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserVersionExists(id))
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

        // POST: api/UserVersions
        [HttpPost]
        public async Task<ActionResult<UserVersion>> PostUserVersion(UserVersion userVersion)
        {
            _context.UserVersions.Add(userVersion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserVersion", new { id = userVersion.UserVersionId }, userVersion);
        }

        // DELETE: api/UserVersions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserVersion>> DeleteUserVersion(int id)
        {
            var userVersion = await _context.UserVersions.FindAsync(id);
            if (userVersion == null)
            {
                return NotFound();
            }

            _context.UserVersions.Remove(userVersion);
            await _context.SaveChangesAsync();

            return userVersion;
        }

        private bool UserVersionExists(int id)
        {
            return _context.UserVersions.Any(e => e.UserVersionId == id);
        }
    }
}
