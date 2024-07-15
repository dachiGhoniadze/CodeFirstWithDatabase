using CodeFirstWithDatabase.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstWithDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly CMSContext _cmsContext;
        public PrescriptionController(CMSContext CMSContext)
        {
            _cmsContext = CMSContext;
        }

        // GET: api/Prescription
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptions()
        {
            if (_cmsContext.Prescriptions == null)
            {
                return NotFound();
            }
            return await _cmsContext.Prescriptions.ToListAsync();
        }

        // GET: api/Prescription/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Prescription>> GetPrescription(int id)
        {
            if (_cmsContext.Prescriptions is null)
            {
                return NotFound();
            }
            var prescription = await _cmsContext.Prescriptions.FindAsync(id);
            if (prescription is null)
            {
                return NotFound();
            }
            return prescription;
        }

        // Put : api/Prescriptions/2
        [HttpPut]
        public async Task<ActionResult<Prescription>> PutPrescription(int id, Prescription prescription)
        {
            if (id != prescription.PrescriptionID)
            {
                return BadRequest();
            }
            _cmsContext.Entry(prescription).State = EntityState.Modified;
            try
            {
                await _cmsContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        private bool PrescriptionExists(long id)
        {
            return (_cmsContext.Prescriptions?.Any(prescription => prescription.PrescriptionID == id)).GetValueOrDefault();
        }

        // Post : api/Prescriptions
        [HttpPost]
        public async Task<ActionResult<Prescription>> PostPrescription(Prescription prescription)
        {
            _cmsContext.Prescriptions.Add(prescription);
            await _cmsContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPrescription), new { id = prescription.PrescriptionID }, prescription);
        }

        // Delete : api/Prescriptions/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Prescription>> DeletePrescription(int id)
        {
            if (_cmsContext.Prescriptions is null)
            {
                return NotFound();
            }
            var prescription = await _cmsContext.Prescriptions.FindAsync(id);
            if (prescription is null)
            {
                return NotFound();
            }
            _cmsContext.Prescriptions.Remove(prescription);
            await _cmsContext.SaveChangesAsync();
            return NoContent();
        }



    }
}
