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
using Version = PluginSleuth.Models.Version;

namespace PluginSleuth.Controllers
{
    public class VersionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public VersionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Versions
        public async Task<IActionResult> Index(int? id)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            //receive Id of plugin from the action link.
            var pId = Convert.ToInt32(id);

            ViewBag.PluginId = pId;

            //filter plugins  by the plugin id.
            var applicationDbContext = _context.Versions.Where(v => v.PluginId == pId).Include(v => v.Plugin);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Versions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");


            var version = await _context.Versions
                .Include(v => v.Plugin)
                .FirstOrDefaultAsync(m => m.VersionId == id);
            if (version == null)
            {
                return NotFound();
            }

            return View(version);
        }

        // GET: Versions/Create
        public IActionResult Create(int? id)
        {
            var pId = Convert.ToInt32(id);

            //pass pluginId to view bag so the back button can navigate back to list.
            ViewBag.Pid = pId;

            //make the plugin Id the only choice.
            ViewData["PluginId"] = new SelectList(_context.Plugins.Where(p => p.PluginId == pId), "PluginId", "Title");
            return View();
        }

        // POST: Versions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VersionId,Name,ReadMe,PluginId,DownloadLink,Iteration")] Version version)
        {

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                //set iteration to one plus the number of versions that already have this version's pluginId.
                version.Iteration = _context.Versions.Where(v => v.PluginId == version.PluginId).Count() + 1;
                _context.Add(version);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                //go to plugin details when creating new version.
                return RedirectToAction("Details", "Plugins", new { id = version.PluginId });
            }
            ViewData["PluginId"] = new SelectList(_context.Plugins, "PluginId", "Title", version.PluginId);
            return View(version);
        }

        // GET: Versions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var version = await _context.Versions.FindAsync(id);
            if (version == null)
            {
                return NotFound();
            }
            ViewData["PluginId"] = new SelectList(_context.Plugins.Where(p => p.PluginId == version.PluginId), "PluginId", "Title");
            return View(version);
        }

        // POST: Versions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VersionId,Name,ReadMe,PluginId,DownloadLink,Iteration")] Version version)
        {
            if (id != version.VersionId)
            {
                return NotFound();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");


            if (ModelState.IsValid)
            {
                try
                {
                    //get iteration from original version, but remember to remove the oriignal version from _context or there will be errors resulting from duplicate Ids.
                    var OldVersion = _context.Versions.FirstOrDefault(v => v.VersionId == version.VersionId);
                    _context.Remove(OldVersion);
                    version.Iteration = OldVersion.Iteration;
                    _context.Update(version);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VersionExists(version.VersionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //redirect back to plugin details list.
                return RedirectToAction("Index", "Versions", new { id = version.PluginId });
            }
            ViewData["PluginId"] = new SelectList(_context.Plugins, "PluginId", "Title", version.PluginId);
            return View(version);
        }

        // GET: Versions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var version = await _context.Versions
                .Include(v => v.Plugin)
                .FirstOrDefaultAsync(m => m.VersionId == id);
            if (version == null)
            {
                return NotFound();
            }

            return View(version);
        }

        // POST: Versions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var version = await _context.Versions.FindAsync(id);
            _context.Versions.Remove(version);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Versions", new { id = version.PluginId });
        }

        private bool VersionExists(int id)
        {
            return _context.Versions.Any(e => e.VersionId == id);
        }
    }
}
