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
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["VersionId"] = new SelectList(_context.Versions, "VersionId", "Name");
            return View();
        }

        // POST: UserVersions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserVersionId,UserId,VersionId")] UserVersion userVersion)
        {

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                _context.Add(userVersion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", userVersion.UserId);
            ViewData["VersionId"] = new SelectList(_context.Versions, "VersionId", "Name", userVersion.VersionId);
            return View(userVersion);
        }

        // GET: UserVersions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userVersion = await _context.UserVersions.FindAsync(id);
            if (userVersion == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", userVersion.UserId);
            ViewData["VersionId"] = new SelectList(_context.Versions, "VersionId", "Name", userVersion.VersionId);
            return View(userVersion);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", userVersion.UserId);
            ViewData["VersionId"] = new SelectList(_context.Versions, "VersionId", "Name", userVersion.VersionId);
            return View(userVersion);
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
