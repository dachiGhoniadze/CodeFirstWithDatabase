using CodeFirstWithDatabase.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstWithDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly CMSContext _cmsContext;
        public PatientController(CMSContext CMSContext)
        {
            _cmsContext = CMSContext;
        }

        // GET: api/Patient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            if (_cmsContext.Patients == null)
            {
                return NotFound();
            }
            return await _cmsContext.Patients.ToListAsync();
        }

        // GET: api/Patient/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            if (_cmsContext.Patients is null)
            {
                return NotFound();
            }
            var patient = await _cmsContext.Patients.FindAsync(id);
            if (patient is null)
            {
                return NotFound();
            }
            return patient;
        }

        // Put : api/Patients/2
        [HttpPut]
        public async Task<ActionResult<Patient>> PutPatient(int id, Patient patient)
        {
            if (id != patient.PatientId)
            {
                return BadRequest();
            }
            _cmsContext.Entry(patient).State = EntityState.Modified;
            try
            {
                await _cmsContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        private bool PatientExists(long id)
        {
            return (_cmsContext.Patients?.Any(patient => patient.PatientId == id)).GetValueOrDefault();
        }

        // Post : api/Patients
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            _cmsContext.Patients.Add(patient);
            await _cmsContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, patient);
        }

        // Delete : api/Patients/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            if (_cmsContext.Patients is null)
            {
                return NotFound();
            }
            var patient = await _cmsContext.Patients.FindAsync(id);
            if (patient is null)
            {
                return NotFound();
            }
            _cmsContext.Patients.Remove(patient);
            await _cmsContext.SaveChangesAsync();
            return NoContent();
        }

        // HEAD: api/Patient
        [HttpHead]
        public IActionResult HeadPatients()
        {
            if (_cmsContext.Patients == null)
            {
                return NotFound();
            }
            return Ok();
        }

        // OPTIONS: api/Patient
        [HttpOptions]
        public IActionResult OptionsPatients()
        {
            Response.Headers.Add("Allow", "GET,POST,HEAD,OPTIONS,PATCH");
            return Ok();
        }

        //// PATCH: api/Patient/2
        //[HttpPatch("{id}")]
        //public async Task<IActionResult> PatchPatient(int id, [FromBody] JsonPatchDocument<Patient> patchDoc)
        //{
        //    if (patchDoc == null)
        //    {
        //        return BadRequest();
        //    }

        //    var patient = await _cmsContext.Patients.FindAsync(id);
        //    if (patient == null)
        //    {
        //        return NotFound();
        //    }

        //    patchDoc.ApplyTo(patient, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        await _cmsContext.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PatientExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //private bool PatientExists(int id)
        //{
        //    return (_cmsContext.Patients?.Any(e => e.PatientId == id)).GetValueOrDefault();
        //}

    }
}
