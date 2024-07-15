using CodeFirstWithDatabase.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstWithDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly CMSContext _cmsContext;
        public PharmacyController(CMSContext CMSContext)
        {
            _cmsContext = CMSContext;
        }

        // GET: api/Pharmacy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pharmacy>>> GetPharmacies()
        {
            if (_cmsContext.Pharmacies == null)
            {
                return NotFound();
            }
            return await _cmsContext.Pharmacies.ToListAsync();
        }

        // GET: api/Pharmacy/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Pharmacy>> GetPharmacy(int id)
        {
            if (_cmsContext.Pharmacies is null)
            {
                return NotFound();
            }
            var pharmacy = await _cmsContext.Pharmacies.FindAsync(id);
            if (pharmacy is null)
            {
                return NotFound();
            }
            return pharmacy;
        }

        // Put : api/Pharmacies/2
        [HttpPut]
        public async Task<ActionResult<Pharmacy>> PutPharmacy(int id, Pharmacy pharmacy)
        {
            if (id != pharmacy.PharmacyId)
            {
                return BadRequest();
            }
            _cmsContext.Entry(pharmacy).State = EntityState.Modified;
            try
            {
                await _cmsContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PharmacyExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        private bool PharmacyExists(long id)
        {
            return (_cmsContext.Pharmacies?.Any(pharmacy => pharmacy.PharmacyId == id)).GetValueOrDefault();
        }

        // Post : api/Pharmacies
        [HttpPost]
        public async Task<ActionResult<Pharmacy>> PostPharmacy(Pharmacy pharmacy)
        {
            _cmsContext.Pharmacies.Add(pharmacy);
            await _cmsContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPharmacy), new { id = pharmacy.PharmacyId }, pharmacy);
        }

        // Delete : api/Pharmacies/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pharmacy>> DeletePharmacy(int id)
        {
            if (_cmsContext.Pharmacies is null)
            {
                return NotFound();
            }
            var pharmacy = await _cmsContext.Pharmacies.FindAsync(id);
            if (pharmacy is null)
            {
                return NotFound();
            }
            _cmsContext.Pharmacies.Remove(pharmacy);
            await _cmsContext.SaveChangesAsync();
            return NoContent();
        }





    }
}
