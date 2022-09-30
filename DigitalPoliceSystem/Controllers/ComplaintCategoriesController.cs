using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DigitalPoliceSystem.Data;
using DigitalPoliceSystem.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.AspNetCore.Authorization;

namespace DigitalPoliceSystem.Controllers
{
    /// <remarks>
    ///     In an ASP.NET Core REST API, there is no need to explicitly check if the model state is Valid. 
    ///     Since the controller class is decorated with the [ApiController] attribute, 
    ///     it takes care of checking if the model state is valid 
    ///     and automatically returns 400 response along the validation errors.
    ///     Example response:
    ///         {
    ///             "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    ///             "title": "One or more validation errors occurred.",
    ///             "status": 400,
    ///             "traceId": "|65b7c07c-4323622998dd3b3a.",
    ///             "errors": {
    ///                 "Email": [
    ///                     "The Email field is required."
    ///                 ],
    ///                 "FirstName": [
    ///                     "The field FirstName must be a string with a minimum length of 2 and a maximum length of 100."
    ///                 ]
    ///             }
    ///         }
    /// </remarks>
    /// 

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "PoliceAdmin")]
    public class ComplaintCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ComplaintCategoriesController> _logger;

        public ComplaintCategoriesController(ApplicationDbContext context, ILogger<ComplaintCategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/ComplaintCategories
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<ComplaintCategory>>> GetComplaintCategories()
        public async Task<IActionResult> GetComplaintCategories()
        {
            try
            {
                var complaintCategories = await _context.ComplaintCategories.Include(c => c.Complaints).ToListAsync();

                // Check if data exists in the Database
                if (complaintCategories == null)
                {
                    return NotFound();                      // RETURN: No data was found            HTTP 404
                }
                return Ok(complaintCategories);             // RETURN: OkObjectResult - good result HTTP 200
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);             // RETURN: BadResult                    HTTP 400
            }

            //return await _context.ComplaintCategories.Include(c => c.Complaints).ToListAsync();
        }

        // GET: api/ComplaintCategories/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<ComplaintCategory>> GetComplaintCategory(int? id)

        public async Task<ActionResult> GetComplaintCategory(int? id)
        {

            if (!id.HasValue)
            {
                return BadRequest();            // RETURN: BadResult                    HTTP 400
            }

            try
            {

                var complaintCategory = await _context.ComplaintCategories.FindAsync(id);

                if (complaintCategory == null)
                {
                    return NotFound();      // RETURN: No data was found            HTTP 404
                }

                return Ok(complaintCategory);       // RETURN: OkObjectResult - good result HTTP 200
            }

            catch (Exception exp)
            {
                return BadRequest(exp.Message);             
            }
        }

        // PUT: api/ComplaintCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplaintCategory(int id, ComplaintCategory complaintCategory)
        {
            if (id != complaintCategory.ComplaintCategoryId)
            {
                return BadRequest();                // RETURN: BadResult                    HTTP 400
            }

            // Sanitize the Data
            complaintCategory.CompliantCategoryName = complaintCategory.CompliantCategoryName.Trim();

            // Server Side Validation
            bool isDuplicateFound = _context.ComplaintCategories.Any(c => c.CompliantCategoryName == complaintCategory.CompliantCategoryName);
            if (isDuplicateFound)
            {
                ModelState.AddModelError("POST", "Duplicate Compliant Category Found!");
            }


            if (ModelState.IsValid)
            {

                try
                {
                    _context.Entry(complaintCategory).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return NoContent();                 
                }
                catch (DbUpdateConcurrencyException exp)
                {
                    if (!ComplaintCategoryExists(id))
                    {
                        return NotFound();              // RETURN: No data was found            HTTP 404
                    }
                    else
                    {
                        //throw;
                        ModelState.AddModelError("PUT", exp.Message);
                    }
                }
            }
            return BadRequest(ModelState);              // RETURN: BadResult                    HTTP 400
            //return NoContent();
        }

        // POST: api/ComplaintCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        //public async Task<ActionResult<ComplaintCategory>> PostComplaintCategory(ComplaintCategory complaintCategory)
        public async Task<ActionResult> PostComplaintCategory(ComplaintCategory complaintCategory)
        {
            // Sanitize the Data
            complaintCategory.CompliantCategoryName = complaintCategory.CompliantCategoryName.Trim();

            // Server Side Validation
            bool isDuplicateFound = _context.ComplaintCategories.Any(c => c.CompliantCategoryName == complaintCategory.CompliantCategoryName);
            if (isDuplicateFound)
            {
                ModelState.AddModelError("POST", "Duplicate Compliant Category Found!");
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.ComplaintCategories.Add(complaintCategory);
                    await _context.SaveChangesAsync();
                    // To enforce that the HTTP RESPONSE CODE 201 "CREATED", package the response.
                    var result = CreatedAtAction("GetComplaintCategory", new { id = complaintCategory.ComplaintCategoryId }, complaintCategory);
                    return Ok(result);
                }
                catch (System.Exception exp)
                {
                    ModelState.AddModelError("POST", exp.Message);
                }
            }
            return BadRequest(ModelState);          // RETURN: BadResult                    HTTP 400
        }

        // DELETE: api/ComplaintCategories/5
        [HttpDelete("{id}")]
        //public async Task<ActionResult<ComplaintCategory>> DeleteComplaintCategory(int id)
        public async Task<ActionResult> DeleteComplaintCategory(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();            // RETURN: BadResult                    HTTP 400
            }

            try
            {

                var complaintCategory = await _context.ComplaintCategories.FindAsync(id);
                if (complaintCategory == null)
                {
                    return NotFound();
                }

                _context.ComplaintCategories.Remove(complaintCategory);
                await _context.SaveChangesAsync();

                return Ok(complaintCategory);                   // RETURN: OkObjectResult - good result HTTP 200
            }
            catch (System.Exception exp)
            {
                ModelState.AddModelError("DELETE", exp.Message);
                return BadRequest(ModelState);                  // RETURN: BadResult                    HTTP 400
            }
        }

        private bool ComplaintCategoryExists(int id)
        {
            return _context.ComplaintCategories.Any(e => e.ComplaintCategoryId == id);
        }
    }
}
