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
    public class UnlistPluginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnlistPluginController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: UnlistPlugin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //the sole purpose of this method is to call the unlist method and return its result.
            return await Unlist(Convert.ToInt32(id));
        }

        // POST: Plugins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Unlist(int id)
        {
            var plugin = await _context.Plugins.FirstOrDefaultAsync(p => p.PluginId == id);

            if (plugin == null)
            {
                return NotFound();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");


            if (ModelState.IsValid)
            {
                try
                {   //toggle isList for plugin.
                    if (plugin.IsListed == true)
                    {
                        plugin.IsListed = false;
                    }
                    else
                    {
                        plugin.IsListed = true;
                    }
                    //update ad save changes
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
            }
            return RedirectToAction("Index", "Plugins", new { id = plugin.PluginId });

        }

        private bool PluginExists(int id)
        {
            return _context.Plugins.Any(e => e.PluginId == id);
        }


    }
}
