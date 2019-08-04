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
    public class PluginsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public PluginsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task BagSearchItems()
        {
            //this controller gets Engine and PluginType data, which gets placed in a viewBag for use by the plugin search feature in _Layaout. The Engines and Plugin Types are used in a dropdown to narrow the search.
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            //get engines and plugin types
            var engines = await _context.Engines.ToListAsync();
            var pluginTypes = await _context.PluginTypes.ToListAsync();

            //add to view bag.
            ViewBag.Engines = engines;
            ViewBag.PluginTypes = pluginTypes;
        }


    // GET: Plugins filtered by the user's authored plugins.
    public async Task<IActionResult> Index()
        {
            //gets the search terms for the search bar in layout.
            await BagSearchItems();

            var currentUser = await GetCurrentUserAsync();


            ModelState.Remove("UserId");
            ModelState.Remove("User");

            //get a list of plugins, filtered by the current user Id.

            if (currentUser == null)
            {
                return NotFound();
            }

            var plugins = _context.Plugins.Where(p => p.UserId == currentUser.Id);

            if (plugins.Count() == 0)
            {
                return NotFound();
            }
            else  
            {
                await plugins.Include(p => p.Engine).Include(p => p.PluginType).Include(p => p.User).ToListAsync();
                return View(plugins);
            }
        }

        //Index filtered by downloaded status.  
        public async Task<IActionResult> IndexDownloaded()
        {
            //gets the search terms for the search bar in layout.
            await BagSearchItems();

            var currentUser = await GetCurrentUserAsync();


            ModelState.Remove("UserId");
            ModelState.Remove("User");

            //get a list of plugins, filtered by the current user Id.

            if (currentUser == null)
            {
                return NotFound();
            }

            //get all plugins
            var plugins = _context.Plugins;

            //if no plugins, return not found.
            if (plugins.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                //get all user versions that match the user(with version included). This is the user's link to the plugins.
                var userVersions = _context.UserVersions.Include(uv => uv.Version).Where(uv => uv.UserId == currentUser.Id);

                //if no userVersions matching, return Not Found.
                if (userVersions.Count() == 0)
                {
                    return NotFound();
                } else
                {
                    //loop through userVersions, get a list of unique PluginIds.
                    var pluginIds = userVersions.Select(uv => uv.Version.PluginId).Distinct().ToArray();
                    //return list of plugins where pluginIds match one of the ids in the PluginIds array.
                    var filteredPlugins = plugins.Where(p => p.IsListed == true).Where(p => Array.Exists(pluginIds, element => element == p.PluginId));
                    //Include the navigational properties of the filtered set and convert to list.
                    var pluginList = await filteredPlugins.Include(p => p.Engine).Include(p => p.PluginType).Include(p => p.User).ToListAsync();
                    return View(pluginList);
                }
            }
        }

        // GET: Plugins filtered by search parameters.
        public async Task<IActionResult> IndexSearch(string searchString, string searchByUsage, string searchByEngine, string searchByType, string searchByPaid)
        {
            //gets the search terms for the search bar in layout.
            await BagSearchItems();

            ViewData["searchByUsage"] = searchByUsage;
            ViewData["CurrentFilter"] = searchString;
            ViewData["searchByEngine"] = searchByEngine;
            ViewData["searchByType"] = searchByType;
            ViewData["searchByPaid"] = searchByPaid;

            int searchUsage = Convert.ToInt32(searchByUsage);
            int searchEngine = Convert.ToInt32(searchByEngine);
            int searchType = Convert.ToInt32(searchByType);
            int searchPaid = Convert.ToInt32(searchByPaid);

            var plugins = _context.Plugins;

            IQueryable<Plugin> pluginQueries = plugins;


            //Search without an query in the nav bar (still narrows by dropdown values).
            if (!String.IsNullOrEmpty(searchString))
            {
                pluginQueries = pluginQueries.Where(p => p.Title.Contains(searchString));
            }
            if (searchUsage != 0)
            {
                //only search by usage restriciton if it isn't 0 (no restrictions).
                pluginQueries = pluginQueries.Where(p => p.CommercialUse == searchUsage);
            }
            if (searchPaid == 1)
            {
                //if search is restricted to free plugins, filters all pay-to-download plugins.
                pluginQueries = pluginQueries.Where(p => p.Free == true);
            }
            //get navigational properties, return in view as a list.
            pluginQueries = pluginQueries.Where(p => p.EngineId == searchEngine && p.PluginTypeId == searchType);
            return View(await pluginQueries.Include(p => p.Engine).Include(p => p.PluginType)
                        .Include(p => p.User).ToListAsync());
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
            ViewData["EngineId"] = new SelectList(_context.Engines, "EngineId", "Title");
            ViewData["PluginTypeId"] = new SelectList(_context.PluginTypes, "PluginTypeId", "Name");
            return View();
        }

        // POST: Plugins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PluginId,Title,UserId,EngineId,PluginTypeId,CommercialUse,Free,Webpage,IsListed")] Plugin plugin)
        {
            var currentUser = await GetCurrentUserAsync();

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                plugin.UserId = currentUser.Id;
                plugin.IsListed = true;
                _context.Add(plugin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EngineId"] = new SelectList(_context.Engines, "EngineId", "Title", plugin.EngineId);
            ViewData["PluginTypeId"] = new SelectList(_context.PluginTypes, "PluginTypeId", "Name", plugin.PluginTypeId);
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
            ViewData["EngineId"] = new SelectList(_context.Engines, "EngineId", "Title", plugin.EngineId);
            ViewData["PluginTypeId"] = new SelectList(_context.PluginTypes, "PluginTypeId", "Name", plugin.PluginTypeId);
            return View(plugin);
        }

        // POST: Plugins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PluginId,Title,UserId,EngineId,PluginTypeId,CommercialUse,Free,Webpage,IsListed")] Plugin plugin)
        {
            var currentUser = await GetCurrentUserAsync();

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
                    plugin.UserId = currentUser.Id;
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
