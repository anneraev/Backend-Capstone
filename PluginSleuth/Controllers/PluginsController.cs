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
using PluginSleuth.Models.PluginViews;
using Version = PluginSleuth.Models.Version;
using System.Web;

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

            if (currentUser != null)
            {
                ViewBag.UserId = currentUser.Id;
                ViewBag.Admin = currentUser.IsAdmin;
            }
            else
            {
                ViewBag.UserId = "0";
                ViewBag.Admin = false;
            }

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
                    IQueryable<Plugin> filteredPlugins = plugins.Where(p => Array.Exists(pluginIds, element => element == p.PluginId));
                    //filters out unlisted plugins if the user is not an admin (admins can see unlisted plugins).
                    if (currentUser.IsAdmin == false) {
                        filteredPlugins = filteredPlugins.Where(p => p.IsListed == true);
                    }
                    //for each plugin, determine if an associated user version is hidden, and if so, remove it from the list.
                    await filteredPlugins.ForEachAsync(p =>
                    {
                        //find the first plugin user version.
                        var pluginUserVersion = userVersions.Where(uv => uv.Version.PluginId == p.PluginId).OrderBy(uv => uv.UserVersionId).FirstOrDefault();
                        //remove if hidden.
                        if (pluginUserVersion.Hidden == true)
                        {
                            filteredPlugins = filteredPlugins.Where(pu => pu.PluginId != pluginUserVersion.Version.PluginId);
                        }

                    });
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

            var currentUser = await GetCurrentUserAsync();

            if (currentUser != null)
            {
                ViewBag.UserId = currentUser.Id;
                ViewBag.Admin = currentUser.IsAdmin;
            } else
            {
                ViewBag.UserId = "0";
                ViewBag.Admin = false;
            }

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

            //filters out unlisted plugins if the user is not an admin (admins can see unlisted plugins).
            if (currentUser == null || currentUser.IsAdmin == false)
            {

                pluginQueries = pluginQueries.Where(p => p.IsListed == true);
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
            //filter down to the plugins that match the EngineId and PluginId of those respective search parameters.
            pluginQueries = pluginQueries.Where(p => p.EngineId == searchEngine && p.PluginTypeId == searchType);

            if (!String.IsNullOrEmpty(searchString))
            {
                await pluginQueries.ForEachAsync(p =>
                {
                    if (p.Keywords != "" && p.Keywords != null)
                    {
                        //get keyword hash
                        var keywordHash = p.Keywords.Split(";");
                        //if search string doesn't contain any of the keywords, and does not appear in the title, it is filtered out.
                        if (!keywordHash.Any(k => searchString.Contains(k)) && !p.Title.Contains(searchString))
                        {
                            pluginQueries = pluginQueries.Where(pu => pu.PluginId != p.PluginId);
                        }
                    } else
                    { //filter by title string only.
                        if (!p.Title.Contains(searchString))
                        {
                            pluginQueries = pluginQueries.Where(pu => pu.PluginId != p.PluginId);
                        }

                    }
                });
            }


            //get navigational properties, return in view as a list.
            return View(await pluginQueries.Include(p => p.Engine).Include(p => p.PluginType)
                        .Include(p => p.User).ToListAsync());
        }



        // GET: Plugins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var currentUser = await GetCurrentUserAsync();

            //get query string value i.e.: "?version=1"
            var query = Request.QueryString.ToString();

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

            //get all associated versions, ordered in descending order by their iteration #s.
            var versions = _context.Versions.Where(v => v.PluginId == id).OrderByDescending(v => v.Iteration);

            if (plugin == null)
            {
                return NotFound();
            }

            //add webpage url to viewbag (using @model.displayfor as the url causes bugs)
            if (plugin.Webpage != null)
            {
                ViewBag.Url = plugin.Webpage;
            } else
            {
                ViewBag.Url = "";  
            }

            //strongly typed local variable to be set in the following if/else statement.
            Version currentVersion;

            //set the current display version by the url string, or set it to the latest by default if none exists.
            if (query.Contains("version"))
            {
                //get version # as string from url query string
                var versionString = HttpUtility.ParseQueryString(query).Get("version");

                //convert string to int
                var versionNum = Convert.ToInt32(versionString);

                //compare into to version ids of all versions and get the one that matches, set it as the currentVersion.
                currentVersion = versions.FirstOrDefault(v => v.VersionId == versionNum);

            } else
            {
                //if no version Id can be derrived from the url query, select the first (latest iteration).
                currentVersion = versions.FirstOrDefault(); 
            }

            if (currentVersion != null && currentVersion.DownloadLink != null)
            {
                ViewBag.Vurl = currentVersion.DownloadLink;
            }
            else
            {
                ViewBag.Vurl = "";
            }

            //get user version if one exists, otherwise it can be set to null.
            UserVersion userVersion = null;
            //start by finding all that match userId, then including their version.
            var userVersions = _context.UserVersions.Where(uv => uv.UserId == currentUser.Id).Include(uv => uv.Version);
            //get the pluginId from the int? that was passed to this method.
            var pluginId = Convert.ToInt32(id);
            //filter userVersions by the pluginId where it matches the version's plugin Id.

            IQueryable<UserVersion> matchingUserVersions = null;

            if (currentUser != null && userVersions != null)
            {
                matchingUserVersions = userVersions.Where(uv => uv.Version.PluginId == pluginId);
                if (matchingUserVersions != null)
                {
                    if (matchingUserVersions.Count() > 1)
                    {
                        //always get the earliest (original) user version.
                        userVersion = matchingUserVersions.OrderBy(uv => uv.VersionId).FirstOrDefault();

                    } else if (matchingUserVersions.Count() == 1)
                    {
                        userVersion = matchingUserVersions.FirstOrDefault();
                    }
                }
               }
            //instantiate view model and add the version list, current version and plugin to it.

            string currentUserId;

            if (currentUser != null)
            {
                currentUserId = currentUser.Id;
            } else
            {
                currentUserId = "0";
            }

            var modelView = new PluginVersionModelView()
            {
                Versions = versions.ToList(),

                Plugin = plugin,

                CurrentVersion = currentVersion,

                UserVersion = userVersion,

                currentUserId = currentUserId
            };

            //return the view model.
            return View(modelView);
        }
        
        // GET: Plugins/Create
        public async Task<IActionResult> Create()
        {

            var currentUser = await GetCurrentUserAsync();

            if (currentUser == null)
            {
                return NotFound();
            }

            ViewData["EngineId"] = new SelectList(_context.Engines, "EngineId", "Title");
            ViewData["PluginTypeId"] = new SelectList(_context.PluginTypes, "PluginTypeId", "Name");
            return View();
        }

        // POST: Plugins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PluginId,Title,UserId,EngineId,PluginTypeId,CommercialUse,Free,Webpage,IsListed,About,Keywords")] Plugin plugin)
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
                //create inital version
                var version = new Version()
                {
                    Name = "Initial",
                    PluginId = plugin.PluginId
                };
                //save initial version.
                _context.Add(version);
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers.Where(u => u.Id == plugin.UserId), "Id", "Name", plugin.UserId); ;
            return View(plugin);
        }

        // POST: Plugins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PluginId,Title,UserId,EngineId,PluginTypeId,CommercialUse,Free,Webpage,IsListed,About,Keywords")] Plugin plugin)
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
                {   //set IsList to the value of that property on the previous instance of this plugin.
                    var oldPlugin = _context.Plugins.FirstOrDefault(p => p.PluginId == plugin.PluginId);
                    _context.Remove(oldPlugin);
                    plugin.IsListed = oldPlugin.IsListed;
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
            //get all versions matching the current PluginId.
            var versions = _context.Versions.Where(v => v.PluginId == id);

            //create an array of just the version Ids.
            var versionIds = versions.Select(v => v.VersionId).ToArray();

            //get all user versions which match one of the list of version Ids.
            var userVersions = _context.UserVersions.Where(uv => Array.Exists(versionIds, element => element == uv.VersionId));

            //remove the list of user versions.
            _context.RemoveRange(userVersions);
            await _context.SaveChangesAsync();

            //remove the list of versions
            _context.RemoveRange(versions);
            await _context.SaveChangesAsync();

            //finally, remove the plugin (now free of foreign key restrictions).
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
