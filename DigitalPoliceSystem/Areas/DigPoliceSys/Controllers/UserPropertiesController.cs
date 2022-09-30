using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalPoliceSystem.Data;
using DigitalPoliceSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace DigitalPoliceSystem.Areas.DigPoliceSys.Controllers
{
    [Area("DigPoliceSys")]
    public class UserPropertiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserPropertiesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DigPoliceSys/UserProperties
        public async Task<IActionResult> Index()
        {
            var viewmodel = await _context.UserProperty
                                          .Include(u => u.Complaints)
                                          .ToListAsync();

            //return View(await _context.UserProperty.ToListAsync());
            return View(viewmodel);
        }

        // GET: DigPoliceSys/UserProperties/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProperty = await _context.UserProperty
                .FirstOrDefaultAsync(m => m.UserPropertyId == id);
            if (userProperty == null)
            {
                return NotFound();
            }

            return View(viewName: "DetailsUser", model: userProperty);
        }

        // GET: DigPoliceSys/UserProperties/Details/5
        public async Task<IActionResult> DetailsAdmin(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProperty = await _context.UserProperty
                .FirstOrDefaultAsync(m => m.UserPropertyId == id);
            if (userProperty == null)
            {
                return NotFound();
            }

            return View(viewName: "Details", model: userProperty);
        }

        // GET: DigPoliceSys/UserProperties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DigPoliceSys/UserProperties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserPropertyName,UserPropertyPhoneNumer,UserPropertyAddress")] UserProperty userProperty)  //UserPropertyId,UserPropertyEmailId,
        {
            //string returnUrl = null;
            //returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var userId = await _userManager.GetUserAsync(this.User);

                userProperty.UserPropertyId = userId.Id;
                userProperty.UserPropertyEmailId = userId.Email;

                _context.Add(userProperty);
                await _context.SaveChangesAsync();
                /*return RedirectToAction(nameof(Index));*/
                //RedirectToAction(actionName: "Home", controllerName: "Index");
                //return Redirect("~/LoginConfirmation.html");
                return View("LoginConfirmation");
            }
            return View(userProperty);
        }

        // GET: DigPoliceSys/UserProperties/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProperty = await _context.UserProperty.FindAsync(id);
            if (userProperty == null)
            {
                return NotFound();
            }
            return View(userProperty);
        }

        // POST: DigPoliceSys/UserProperties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserPropertyId,UserPropertyEmailId,UserPropertyName,UserPropertyPhoneNumer,UserPropertyAddress")] UserProperty userProperty)
        {
            if (id != userProperty.UserPropertyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProperty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPropertyExists(userProperty.UserPropertyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                //return RedirectToAction(actionName: "Details",
                //            controllerName: "UserProperties",
                //            routeValues: new { area = "DigPoliceSys" });

                //return Redirect("~/DigPoliceSys/UserProperties/Details/id");
                return View("ChangesSavedSuccessfully");
            }
            return View(userProperty);
        }

        // GET: DigPoliceSys/UserProperties/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProperty = await _context.UserProperty
                .FirstOrDefaultAsync(m => m.UserPropertyId == id);
            if (userProperty == null)
            {
                return NotFound();
            }

            return View(userProperty);
        }

        // POST: DigPoliceSys/UserProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userProperty = await _context.UserProperty.FindAsync(id);
            _context.UserProperty.Remove(userProperty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPropertyExists(string id)
        {
            return _context.UserProperty.Any(e => e.UserPropertyId == id);
        }
    }
}
