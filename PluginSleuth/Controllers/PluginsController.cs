using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PluginSleuth.Data;
using PluginSleuth.Models;

namespace PluginSleuth.Controllers
{
    public class PluginsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PluginsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Plugins
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Plugins.Include(p => p.Engine).Include(p => p.PluginType).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Plugins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");


            var plugin = await _context.Plugins
                .Include(p => p.Engine)
                .Include(p => p.PluginType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PluginId == id);
            if (plugin == null)
            {
                return NotFound();
            }

            return View(plugin);
        }

        // GET: Plugins/Create
        public IActionResult Create()
        {
            ViewData["EngineId"] = new SelectList(_context.Engines, "EngineId", "About");
            ViewData["PluginTypeId"] = new SelectList(_context.PluginTypes, "PluginTypeId", "Name");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Plugins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PluginId,Title,UserId,EngineId,PluginTypeId,CommercialUse,Free,Webpage,IsListed")] Plugin plugin)
        {

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                _context.Add(plugin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EngineId"] = new SelectList(_context.Engines, "EngineId", "About", plugin.EngineId);
            ViewData["PluginTypeId"] = new SelectList(_context.PluginTypes, "PluginTypeId", "Name", plugin.PluginTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", plugin.UserId);
            return View(plugin);
        }

        // GET: Plugins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plugin = await _context.Plugins.FindAsync(id);
            if (plugin == null)
            {
                return NotFound();
            }
            ViewData["EngineId"] = new SelectList(_context.Engines, "EngineId", "About", plugin.EngineId);
            ViewData["PluginTypeId"] = new SelectList(_context.PluginTypes, "PluginTypeId", "Name", plugin.PluginTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", plugin.UserId);
            return View(plugin);
        }

        // POST: Plugins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PluginId,Title,UserId,EngineId,PluginTypeId,CommercialUse,Free,Webpage,IsListed")] Plugin plugin)
        {
            if (id != plugin.PluginId)
            {
                return NotFound();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plugin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PluginExists(plugin.PluginId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EngineId"] = new SelectList(_context.Engines, "EngineId", "About", plugin.EngineId);
            ViewData["PluginTypeId"] = new SelectList(_context.PluginTypes, "PluginTypeId", "Name", plugin.PluginTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", plugin.UserId);
            return View(plugin);
        }

        // GET: Plugins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plugin = await _context.Plugins
                .Include(p => p.Engine)
                .Include(p => p.PluginType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PluginId == id);
            if (plugin == null)
            {
                return NotFound();
            }

            return View(plugin);
        }

        // POST: Plugins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plugin = await _context.Plugins.FindAsync(id);
            _context.Plugins.Remove(plugin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PluginExists(int id)
        {
            return _context.Plugins.Any(e => e.PluginId == id);
        }
    }
}
