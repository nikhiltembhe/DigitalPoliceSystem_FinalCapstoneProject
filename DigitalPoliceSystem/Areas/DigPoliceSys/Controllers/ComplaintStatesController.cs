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
    public class ComplaintStatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplaintStatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DigPoliceSys/ComplaintStates
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComplaintState.ToListAsync());
        }

        // GET: DigPoliceSys/ComplaintStates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintState = await _context.ComplaintState
                .FirstOrDefaultAsync(m => m.ComplaintStateId == id);
            if (complaintState == null)
            {
                return NotFound();
            }

            return View(complaintState);
        }

        // GET: DigPoliceSys/ComplaintStates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DigPoliceSys/ComplaintStates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComplaintStateId,CompliantStateName")] ComplaintState complaintState)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaintState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaintState);
        }

        // GET: DigPoliceSys/ComplaintStates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintState = await _context.ComplaintState.FindAsync(id);
            if (complaintState == null)
            {
                return NotFound();
            }
            return View(complaintState);
        }

        // POST: DigPoliceSys/ComplaintStates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComplaintStateId,CompliantStateName")] ComplaintState complaintState)
        {
            if (id != complaintState.ComplaintStateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintStateExists(complaintState.ComplaintStateId))
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
            return View(complaintState);
        }

        // GET: DigPoliceSys/ComplaintStates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintState = await _context.ComplaintState
                .FirstOrDefaultAsync(m => m.ComplaintStateId == id);
            if (complaintState == null)
            {
                return NotFound();
            }

            return View(complaintState);
        }

        // POST: DigPoliceSys/ComplaintStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaintState = await _context.ComplaintState.FindAsync(id);
            _context.ComplaintState.Remove(complaintState);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintStateExists(int id)
        {
            return _context.ComplaintState.Any(e => e.ComplaintStateId == id);
        }
    }
}
