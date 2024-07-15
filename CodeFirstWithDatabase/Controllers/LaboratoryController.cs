using CodeFirstWithDatabase.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstWithDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoryController : ControllerBase
    {
        private readonly CMSContext _cmsContext;
        public LaboratoryController(CMSContext CMSContext)
        {
            _cmsContext = CMSContext;
        }

        // GET: api/Laboratory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laboratory>>> GetLaboratories()
        {
            if (_cmsContext.Laboratories == null)
            {
                return NotFound();
            }
            return await _cmsContext.Laboratories.ToListAsync();
        }

        // GET: api/Laboratory/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Laboratory>> GetLaboratory(int id)
        {
            if (_cmsContext.Laboratories is null)
            {
                return NotFound();
            }
            var laboratory = await _cmsContext.Laboratories.FindAsync(id);
            if (laboratory is null)
            {
                return NotFound();
            }
            return laboratory;
        }

        // Put : api/Laboratories/2
        [HttpPut]
        public async Task<ActionResult<Laboratory>> PutLaboratory(int id, Laboratory laboratory)
        {
            if (id != laboratory.LaboratoryID)
            {
                return BadRequest();
            }
            _cmsContext.Entry(laboratory).State = EntityState.Modified;
            try
            {
                await _cmsContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LaboratoryExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        private bool LaboratoryExists(long id)
        {
            return (_cmsContext.Laboratories?.Any(laboratory => laboratory.LaboratoryID == id)).GetValueOrDefault();
        }

        // Post : api/Laboratories
        [HttpPost]
        public async Task<ActionResult<Laboratory>> PostLaboratory(Laboratory laboratory)
        {
            _cmsContext.Laboratories.Add(laboratory);
            await _cmsContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLaboratory), new { id = laboratory.LaboratoryID }, laboratory);
        }

        // Delete : api/Laboratories/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Laboratory>> DeleteLaboratory(int id)
        {
            if (_cmsContext.Laboratories is null)
            {
                return NotFound();
            }
            var laboratory = await _cmsContext.Laboratories.FindAsync(id);
            if (laboratory is null)
            {
                return NotFound();
            }
            _cmsContext.Laboratories.Remove(laboratory);
            await _cmsContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
