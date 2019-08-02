using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PluginSleuth.Data;
using PluginSleuth.Models;

namespace PluginSleuth.Controllers
{
    public class PluginTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PluginTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: PluginTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PluginTypes.ToListAsync());
        }

        // GET: PluginTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");


            var pluginType = await _context.PluginTypes
                .FirstOrDefaultAsync(m => m.PluginTypeId == id);
            if (pluginType == null)
            {
                return NotFound();
            }

            return View(pluginType);
        }

        // GET: PluginTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PluginTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PluginTypeId,Name")] PluginType pluginType)
        {

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                _context.Add(pluginType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pluginType);
        }

        // GET: PluginTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pluginType = await _context.PluginTypes.FindAsync(id);
            if (pluginType == null)
            {
                return NotFound();
            }
            return View(pluginType);
        }

        // POST: PluginTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PluginTypeId,Name")] PluginType pluginType)
        {
            if (id != pluginType.PluginTypeId)
            {
                return NotFound();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pluginType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PluginTypeExists(pluginType.PluginTypeId))
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
            return View(pluginType);
        }

        // GET: PluginTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pluginType = await _context.PluginTypes
                .FirstOrDefaultAsync(m => m.PluginTypeId == id);
            if (pluginType == null)
            {
                return NotFound();
            }

            return View(pluginType);
        }

        // POST: PluginTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pluginType = await _context.PluginTypes.FindAsync(id);
            _context.PluginTypes.Remove(pluginType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PluginTypeExists(int id)
        {
            return _context.PluginTypes.Any(e => e.PluginTypeId == id);
        }
    }
}
