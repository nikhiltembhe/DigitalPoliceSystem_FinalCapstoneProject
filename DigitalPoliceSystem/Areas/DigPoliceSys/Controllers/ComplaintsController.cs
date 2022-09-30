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
using Microsoft.AspNetCore.Authorization;

namespace DigitalPoliceSystem.Areas.DigPoliceSys.Controllers
{
    [Area("DigPoliceSys")]
    [Authorize]
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ComplaintsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DigPoliceSys/Complaints
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Complaints.Include(c => c.ComplaintCategory).Include(c => c.ComplaintState).Include(c => c.PoliceStation).Include(c => c.UserProperty);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LibMgmt/GetComplaintsOfUser?filterCategoryId=5
        public async Task<IActionResult> GetComplaintsOfUser(string filterUserPropertyId)
        {
            var viewmodel = await _context.Complaints
                                          .Where(c => c.UserPropertyId == filterUserPropertyId)
                                          .Include(c => c.ComplaintCategory)
                                          .Include(c => c.ComplaintState)
                                          .Include(c => c.PoliceStation)
                                          .Include(c => c.UserProperty)
                                          .ToListAsync();

            return View(viewName: "IndexCustomized", model: viewmodel);
        }

        public async Task<IActionResult> GetComplaintsOfUserAdmin(string filterUserPropertyId)
        {
            var viewmodel = await _context.Complaints
                                          .Where(c => c.UserPropertyId == filterUserPropertyId)
                                          .Include(c => c.ComplaintCategory)
                                          .Include(c => c.ComplaintState)
                                          .Include(c => c.PoliceStation)
                                          .Include(c => c.UserProperty)
                                          .ToListAsync();

            return View(viewName: "Index", model: viewmodel);
        }


        public async Task<IActionResult> GetComplaintsOfPoliceStation(int filterPoliceStaionId)
        {
            var viewmodel = await _context.Complaints
                                          .Where(c => c.PoliceStationId == filterPoliceStaionId)
                                          .Include(c => c.ComplaintCategory)
                                          .Include(c => c.ComplaintState)
                                          .Include(c => c.PoliceStation)
                                          .Include(c => c.UserProperty)
                                          .ToListAsync();

            return View(viewName: "IndexCustomizedAdmin", model: viewmodel);
        }







        // GET: DigPoliceSys/Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.ComplaintCategory)
                .Include(c => c.ComplaintState)
                .Include(c => c.PoliceStation)
                .Include(c => c.UserProperty)
                .FirstOrDefaultAsync(m => m.ComplaintId == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // GET: DigPoliceSys/Complaints/Create
        public IActionResult Create(string id)
        {
            ViewData["ComplaintCategoryId"] = new SelectList(_context.ComplaintCategories, "ComplaintCategoryId", "CompliantCategoryName");
            ViewData["ComplaintStateId"] = new SelectList(_context.ComplaintState, "ComplaintStateId", "CompliantStateName");
            ViewData["PoliceStationId"] = new SelectList(_context.PoliceStation, "PoliceStationId", "PoliceStationName");

            var query = _context.UserProperty.SingleOrDefault(u => u.UserPropertyId == id);
            ViewBag.UserProperty = query.UserPropertyName;
            Complaint c = new Complaint();
            c.UserPropertyId = query.UserPropertyId;
            //ViewData["UserPropertyId"] = new SelectList(_context.UserProperty, "UserPropertyId", "UserPropertyName");
            return View(c);
        }

        // POST: DigPoliceSys/Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComplaintId,ComplaintDescription,IncidentDate,ComplaintCategoryId,PoliceStationId,ComplaintStateId,UserPropertyId")] Complaint complaint)  //UserPropertyId
        {
            //var userId = await _userManager.GetUserAsync(this.User);

            ////userProperty.UserPropertyId = userId.Id;
            ////userProperty.UserPropertyEmailId = userId.Email;

            //complaint.UserPropertyId = userId.Id;

            if (ModelState.IsValid)
            {
                

                _context.Add(complaint);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return View("ComplaintConfirmation");
            }
            ViewData["ComplaintCategoryId"] = new SelectList(_context.ComplaintCategories, "ComplaintCategoryId", "CompliantCategoryName", complaint.ComplaintCategoryId);
            ViewData["ComplaintStateId"] = new SelectList(_context.ComplaintState, "ComplaintStateId", "CompliantStateName", complaint.ComplaintStateId);
            ViewData["PoliceStationId"] = new SelectList(_context.PoliceStation, "PoliceStationId", "PoliceStationName", complaint.PoliceStationId);
            ViewData["UserPropertyId"] = new SelectList(_context.UserProperty, "UserPropertyId", "UserPropertyName", complaint.UserPropertyId);
            return View(complaint);
        }

        // GET: DigPoliceSys/Complaints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }
            ViewData["ComplaintCategoryId"] = new SelectList(_context.ComplaintCategories, "ComplaintCategoryId", "CompliantCategoryName", complaint.ComplaintCategoryId);
            ViewData["ComplaintStateId"] = new SelectList(_context.ComplaintState, "ComplaintStateId", "CompliantStateName", complaint.ComplaintStateId);
            ViewData["PoliceStationId"] = new SelectList(_context.PoliceStation, "PoliceStationId", "PoliceStationName", complaint.PoliceStationId);
            ViewData["UserPropertyId"] = new SelectList(_context.UserProperty, "UserPropertyId", "UserPropertyName", complaint.UserPropertyId);
            return View(complaint);
        }

        // GET: DigPoliceSys/Complaints/Edit/5
        public async Task<IActionResult> EditAdmin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }
            ViewData["ComplaintCategoryId"] = new SelectList(_context.ComplaintCategories, "ComplaintCategoryId", "CompliantCategoryName", complaint.ComplaintCategoryId);
            ViewData["ComplaintStateId"] = new SelectList(_context.ComplaintState, "ComplaintStateId", "CompliantStateName", complaint.ComplaintStateId);
            ViewData["PoliceStationId"] = new SelectList(_context.PoliceStation, "PoliceStationId", "PoliceStationName", complaint.PoliceStationId);
            ViewData["UserPropertyId"] = new SelectList(_context.UserProperty, "UserPropertyId", "UserPropertyName", complaint.UserPropertyId);
            //return View(complaint);
            return View(viewName: "EditAdmin", model: complaint);
        }

        // POST: DigPoliceSys/Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComplaintId,ComplaintDescription,IncidentDate,ComplaintCategoryId,PoliceStationId,ComplaintStateId,UserPropertyId")] Complaint complaint)
        {
            if (id != complaint.ComplaintId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.ComplaintId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return View("ComplaintEdited");
            }
            ViewData["ComplaintCategoryId"] = new SelectList(_context.ComplaintCategories, "ComplaintCategoryId", "CompliantCategoryName", complaint.ComplaintCategoryId);
            ViewData["ComplaintStateId"] = new SelectList(_context.ComplaintState, "ComplaintStateId", "CompliantStateName", complaint.ComplaintStateId);
            ViewData["PoliceStationId"] = new SelectList(_context.PoliceStation, "PoliceStationId", "PoliceStationName", complaint.PoliceStationId);
            ViewData["UserPropertyId"] = new SelectList(_context.UserProperty, "UserPropertyId", "UserPropertyName", complaint.UserPropertyId);
            return View(complaint);
        }

        // POST: DigPoliceSys/Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(int id, [Bind("ComplaintId,ComplaintDescription,IncidentDate,ComplaintCategoryId,PoliceStationId,ComplaintStateId,UserPropertyId")] Complaint complaint)
        {
            if (id != complaint.ComplaintId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.ComplaintId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return View("ComplaintEdited");
            }
            ViewData["ComplaintCategoryId"] = new SelectList(_context.ComplaintCategories, "ComplaintCategoryId", "CompliantCategoryName", complaint.ComplaintCategoryId);
            ViewData["ComplaintStateId"] = new SelectList(_context.ComplaintState, "ComplaintStateId", "CompliantStateName", complaint.ComplaintStateId);
            ViewData["PoliceStationId"] = new SelectList(_context.PoliceStation, "PoliceStationId", "PoliceStationName", complaint.PoliceStationId);
            ViewData["UserPropertyId"] = new SelectList(_context.UserProperty, "UserPropertyId", "UserPropertyName", complaint.UserPropertyId);
            return View(complaint);
        }

        // GET: DigPoliceSys/Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.ComplaintCategory)
                .Include(c => c.ComplaintState)
                .Include(c => c.PoliceStation)
                .Include(c => c.UserProperty)
                .FirstOrDefaultAsync(m => m.ComplaintId == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: DigPoliceSys/Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return View("ComplaintDelete");
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.ComplaintId == id);
        }
    }
}
