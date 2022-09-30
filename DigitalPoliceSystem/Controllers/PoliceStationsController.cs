using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DigitalPoliceSystem.Data;
using DigitalPoliceSystem.Models;

namespace DigitalPoliceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliceStationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PoliceStationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PoliceStations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoliceStation>>> GetPoliceStation()
        {
            return await _context.PoliceStation.Include(p => p.Complaints).ToListAsync();
        }

        // GET: api/PoliceStations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PoliceStation>> GetPoliceStation(int id)
        {
            var policeStation = await _context.PoliceStation.FindAsync(id);

            if (policeStation == null)
            {
                return NotFound();
            }

            return policeStation;
        }

        // PUT: api/PoliceStations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoliceStation(int id, PoliceStation policeStation)
        {
            if (id != policeStation.PoliceStationId)
            {
                return BadRequest();
            }

            // Sanitize the Data
            policeStation.PoliceStationName = policeStation.PoliceStationName.Trim();

            // Server Side Validation
            bool isDuplicateFound = _context.PoliceStation.Any(p => p.PoliceStationName == policeStation.PoliceStationName);
            if (isDuplicateFound)
            {
                ModelState.AddModelError("POST", "Duplicate Police Station Found!");
            }


            if (ModelState.IsValid)
            {

                try
                {
                    _context.Entry(policeStation).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException exp)
                {
                    if (!PoliceStationExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        //throw;
                        ModelState.AddModelError("PUT", exp.Message);
                    }
                }
            }
            return BadRequest(ModelState);
            //return NoContent();
        }

        // POST: api/PoliceStations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PoliceStation>> PostPoliceStation(PoliceStation policeStation)
        {
            // Sanitize the Data
            policeStation.PoliceStationName = policeStation.PoliceStationName.Trim();


            // Server Side Validation
            bool isDuplicateFound = _context.PoliceStation.Any(p => p.PoliceStationName == policeStation.PoliceStationName);
            if (isDuplicateFound)
            {
                ModelState.AddModelError("POST", "Duplicate Police Station Found!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.PoliceStation.Add(policeStation);
                    await _context.SaveChangesAsync();

                    var result = CreatedAtAction("GetPoliceStation", new { id = policeStation.PoliceStationId }, policeStation);
                    return Ok(result);
                }
                catch (System.Exception exp)
                {
                    ModelState.AddModelError("POST", exp.Message);
                }
            }


            return BadRequest(ModelState);
        }

        // DELETE: api/PoliceStations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PoliceStation>> DeletePoliceStation(int id)
        {
            var policeStation = await _context.PoliceStation.FindAsync(id);
            if (policeStation == null)
            {
                return NotFound();
            }

            _context.PoliceStation.Remove(policeStation);
            await _context.SaveChangesAsync();

            return policeStation;
        }

        private bool PoliceStationExists(int id)
        {
            return _context.PoliceStation.Any(e => e.PoliceStationId == id);
        }
    }
}
