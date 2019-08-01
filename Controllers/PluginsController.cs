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
    public class PluginsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PluginsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Plugins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plugin>>> GetPlugins()
        {
            return await _context.Plugins.ToListAsync();
        }

        // GET: api/Plugins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plugin>> GetPlugin(int id)
        {
            var plugin = await _context.Plugins.FindAsync(id);

            if (plugin == null)
            {
                return NotFound();
            }

            return plugin;
        }

        // PUT: api/Plugins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlugin(int id, Plugin plugin)
        {
            if (id != plugin.PluginId)
            {
                return BadRequest();
            }

            _context.Entry(plugin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PluginExists(id))
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

        // POST: api/Plugins
        [HttpPost]
        public async Task<ActionResult<Plugin>> PostPlugin(Plugin plugin)
        {
            _context.Plugins.Add(plugin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlugin", new { id = plugin.PluginId }, plugin);
        }

        // DELETE: api/Plugins/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Plugin>> DeletePlugin(int id)
        {
            var plugin = await _context.Plugins.FindAsync(id);
            if (plugin == null)
            {
                return NotFound();
            }

            _context.Plugins.Remove(plugin);
            await _context.SaveChangesAsync();

            return plugin;
        }

        private bool PluginExists(int id)
        {
            return _context.Plugins.Any(e => e.PluginId == id);
        }
    }
}
