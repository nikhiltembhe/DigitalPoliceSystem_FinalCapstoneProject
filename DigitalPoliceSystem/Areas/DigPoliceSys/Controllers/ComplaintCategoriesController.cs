using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalPoliceSystem.Data;
using DigitalPoliceSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace DigitalPoliceSystem.Areas.DigPoliceSys.Controllers
{
    [Area("DigPoliceSys")]
    [Authorize(Roles = "PoliceAdmin")]
    public class ComplaintCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplaintCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DigPoliceSys/ComplaintCategories
        public async Task<IActionResult> Index()
        {
            var viewmodel = await _context.ComplaintCategories
                                            .Include(p => p.Complaints)
                                            .ToListAsync();
            return View(viewmodel);
            //return View(await _context.ComplaintCategories.ToListAsync());
        }

        // GET: DigPoliceSys/ComplaintCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintCategory = await _context.ComplaintCategories
                .FirstOrDefaultAsync(m => m.ComplaintCategoryId == id);
            if (complaintCategory == null)
            {
                return NotFound();
            }

            return View(complaintCategory);
        }

        // GET: DigPoliceSys/ComplaintCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DigPoliceSys/ComplaintCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComplaintCategoryId,CompliantCategoryName")] ComplaintCategory complaintCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaintCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //return Redirect("~/LoginConfirmation.html");
            }
            return View(complaintCategory);
        }

        // GET: DigPoliceSys/ComplaintCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintCategory = await _context.ComplaintCategories.FindAsync(id);
            if (complaintCategory == null)
            {
                return NotFound();
            }
            return View(complaintCategory);
        }

        // POST: DigPoliceSys/ComplaintCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComplaintCategoryId,CompliantCategoryName")] ComplaintCategory complaintCategory)
        {
            if (id != complaintCategory.ComplaintCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintCategoryExists(complaintCategory.ComplaintCategoryId))
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
            return View(complaintCategory);
        }

        // GET: DigPoliceSys/ComplaintCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintCategory = await _context.ComplaintCategories
                .FirstOrDefaultAsync(m => m.ComplaintCategoryId == id);
            if (complaintCategory == null)
            {
                return NotFound();
            }

            return View(complaintCategory);
        }

        // POST: DigPoliceSys/ComplaintCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaintCategory = await _context.ComplaintCategories.FindAsync(id);
            _context.ComplaintCategories.Remove(complaintCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintCategoryExists(int id)
        {
            return _context.ComplaintCategories.Any(e => e.ComplaintCategoryId == id);
        }
    }
}
