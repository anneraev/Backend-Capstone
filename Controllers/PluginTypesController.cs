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
    public class PluginTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PluginTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PluginTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PluginType>>> GetPluginTypes()
        {
            return await _context.PluginTypes.ToListAsync();
        }

        // GET: api/PluginTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PluginType>> GetPluginType(int id)
        {
            var pluginType = await _context.PluginTypes.FindAsync(id);

            if (pluginType == null)
            {
                return NotFound();
            }

            return pluginType;
        }

        // PUT: api/PluginTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPluginType(int id, PluginType pluginType)
        {
            if (id != pluginType.PluginTypeId)
            {
                return BadRequest();
            }

            _context.Entry(pluginType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PluginTypeExists(id))
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

        // POST: api/PluginTypes
        [HttpPost]
        public async Task<ActionResult<PluginType>> PostPluginType(PluginType pluginType)
        {
            _context.PluginTypes.Add(pluginType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPluginType", new { id = pluginType.PluginTypeId }, pluginType);
        }

        // DELETE: api/PluginTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PluginType>> DeletePluginType(int id)
        {
            var pluginType = await _context.PluginTypes.FindAsync(id);
            if (pluginType == null)
            {
                return NotFound();
            }

            _context.PluginTypes.Remove(pluginType);
            await _context.SaveChangesAsync();

            return pluginType;
        }

        private bool PluginTypeExists(int id)
        {
            return _context.PluginTypes.Any(e => e.PluginTypeId == id);
        }
    }
}
