using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalPoliceSystem.Data;
using DigitalPoliceSystem.Models;

namespace DigitalPoliceSystem.Areas.DigPoliceSys.Controllers
{
    [Area("DigPoliceSys")]
    public class PoliceStationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PoliceStationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DigPoliceSys/PoliceStations
        public async Task<IActionResult> Index()
        {
            var viewmodel = await _context.PoliceStation
                                         .Include(p => p.Complaints)
                                         .ToListAsync();

            //return View(await _context.PoliceStation.ToListAsync());
            return View(viewmodel);
        }

        public async Task<IActionResult> IndexCustomizedAdmin()
        {
            //return View(await applicationDbContext.ToListAsync());
            var viewmodel = await _context.PoliceStation
                                          .Include(p => p.Complaints)
                                          .ToListAsync();
            return View(viewName: "IndexCustomizedAdmin", model: viewmodel);
        }


        public async Task<IActionResult> IndexCustomized()
        {
            //return View(await applicationDbContext.ToListAsync());
            var viewmodel = await _context.PoliceStation.ToListAsync();
            return View(viewName: "IndexCustomized", model: viewmodel);
        }


        // GET: DigPoliceSys/PoliceStations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policeStation = await _context.PoliceStation
                .FirstOrDefaultAsync(m => m.PoliceStationId == id);
            if (policeStation == null)
            {
                return NotFound();
            }

            return View(policeStation);
        }

        // GET: DigPoliceSys/PoliceStations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DigPoliceSys/PoliceStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PoliceStationId,PoliceStationName,PoliceStationPhNo,PoliceStationAddress")] PoliceStation policeStation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policeStation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(policeStation);
        }

        // GET: DigPoliceSys/PoliceStations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policeStation = await _context.PoliceStation.FindAsync(id);
            if (policeStation == null)
            {
                return NotFound();
            }
            return View(policeStation);
        }

        // POST: DigPoliceSys/PoliceStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PoliceStationId,PoliceStationName,PoliceStationPhNo,PoliceStationAddress")] PoliceStation policeStation)
        {
            if (id != policeStation.PoliceStationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policeStation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliceStationExists(policeStation.PoliceStationId))
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
            return View(policeStation);
        }

        // GET: DigPoliceSys/PoliceStations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policeStation = await _context.PoliceStation
                .FirstOrDefaultAsync(m => m.PoliceStationId == id);
            if (policeStation == null)
            {
                return NotFound();
            }

            return View(policeStation);
        }

        // POST: DigPoliceSys/PoliceStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policeStation = await _context.PoliceStation.FindAsync(id);
            _context.PoliceStation.Remove(policeStation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoliceStationExists(int id)
        {
            return _context.PoliceStation.Any(e => e.PoliceStationId == id);
        }
    }
}
