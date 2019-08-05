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
    public class UserVersionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserVersionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);



        // GET: UserVersions
        public async Task<IActionResult> Index()
        {
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            var applicationDbContext = _context.UserVersions.Include(u => u.User).Include(u => u.Version);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserVersions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");


            var userVersion = await _context.UserVersions
                .Include(u => u.User)
                .Include(u => u.Version)
                .FirstOrDefaultAsync(m => m.UserVersionId == id);
            if (userVersion == null)
            {
                return NotFound();
            }

            return View(userVersion);
        }

        // GET: UserVersions/Create
        //calls the second version create using parameters passed to it.
        public async Task<IActionResult> Create(int? id)
        {
            var versionId = Convert.ToInt32(id);
            var userVersion = new UserVersion();
            return await Create(userVersion, versionId);
        }

        // POST: UserVersions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserVersionId,UserId,VersionId")] UserVersion userVersion, int id)
        {

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            //create UserVersion, setting its two parameters without user input and returning to plugin page.
            if (ModelState.IsValid)
            {
                var currentUser = await GetCurrentUserAsync();

                userVersion.VersionId = id;
                userVersion.UserId = currentUser.Id;
                _context.Add(userVersion);
                await _context.SaveChangesAsync();

                //after saving, find plugin that matches the version Id, and store its Id (for navigation back to plugin page).
                var PluginId = _context.Versions.FirstOrDefault(v => v.VersionId == id).PluginId;
                //navigate back to plugin page.
                return RedirectToAction("Details", "Plugins", new { id = PluginId });

            }

            return NotFound();
        }

        // GET: UserVersions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            //pass the original user version to the edit function, where the "Hidden" property will be toggled on or off.
            if (id == null)
            {
                return NotFound();
            }

            var uvId = Convert.ToInt32(id);

            var userVersion = await _context.UserVersions.FindAsync(id);
            if (userVersion == null)
            {
                return NotFound();
            }
            return await Edit(uvId, userVersion);
        }

        // POST: UserVersions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserVersionId,UserId,VersionId")] UserVersion userVersion)
        {
            if (id != userVersion.UserVersionId)
            {
                return NotFound();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");


            if (ModelState.IsValid)
            {
                try
                {
                    if (userVersion.Hidden == true)
                    {
                        userVersion.Hidden = false;
                    } else
                    {
                        userVersion.Hidden = true;
                    }
                    _context.Update(userVersion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserVersionExists(userVersion.UserVersionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //get the pluginId for navigation back to plugin page.
                var PluginId = _context.Versions.FirstOrDefault(v => v.VersionId == userVersion.VersionId).PluginId;
                //navigate back to plugin page.
                return RedirectToAction("Details", "Plugins", new { id = PluginId });

            }
            return NotFound();
        }

        // GET: UserVersions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userVersion = await _context.UserVersions
                .Include(u => u.User)
                .Include(u => u.Version)
                .FirstOrDefaultAsync(m => m.UserVersionId == id);
            if (userVersion == null)
            {
                return NotFound();
            }

            return View(userVersion);
        }

        // POST: UserVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userVersion = await _context.UserVersions.FindAsync(id);
            _context.UserVersions.Remove(userVersion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserVersionExists(int id)
        {
            return _context.UserVersions.Any(e => e.UserVersionId == id);
        }
    }
}
