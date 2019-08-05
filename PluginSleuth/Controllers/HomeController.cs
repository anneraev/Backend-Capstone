using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PluginSleuth.Data;
using PluginSleuth.Models;
using PluginSleuth.Models.HomeViews;

namespace PluginSleuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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


        //gets a list of engine types and a random plugin. Also fills the view bag with Engine items and plugin types for the search bar.
        public async Task<IActionResult> Index()
        {
            await BagSearchItems();

            //gets random featured plugin
            Plugin plugin = await RandomFeaturedPlugin();

            List<Engine> engines = await _context.Engines.ToListAsync();

            //create view model and populate its properties.
            var viewModel = new HomeViewModel
            {
                Plugin = plugin,
                Engines = engines
            };

            if (plugin.Webpage != "" && plugin.Webpage != null)
            {
                ViewBag.Url = plugin.Webpage;
            }
            else
            {
                ViewBag.Url = "";
            }

            //return viewModel
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //Get a random number between two values.
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }


        public async Task<Plugin> RandomFeaturedPlugin()
        {

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            //get all plugins.
            var allPlugns = await _context.Plugins.Where(p => p.IsListed == true).ToListAsync();

            //instantiate plugin in preparation for it to be set.
            Plugin plugin = null;

            //get a random number between 1 and the number of plugins. If this does not result in a found plugin, loop back until one is found.
            while (plugin == null)
            {
                int id = RandomNumber(1, allPlugns.Count + 1);
                //get the plugin which matches that id.
                plugin = await _context.Plugins.Include(p => p.Engine)
                   .Include(p => p.PluginType)
                   .Include(p => p.User)
                    .FirstOrDefaultAsync(m => m.PluginId == id);
            }

            //return plugin once found.
            return plugin;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
