using CodeFirstWithDatabase.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstWithDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly CMSContext _cmsContext;
        public RegistrationController(CMSContext CMSContext)
        {
            _cmsContext = CMSContext;
        }

        // GET: api/Registration
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
        {
            if (_cmsContext.Registrations == null)
            {
                return NotFound();
            }
            return await _cmsContext.Registrations.ToListAsync();
        }

        // GET: api/Registration/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Registration>> GetRegistration(int id)
        {
            if (_cmsContext.Registrations is null)
            {
                return NotFound();
            }
            var registration = await _cmsContext.Registrations.FindAsync(id);
            if (registration is null)
            {
                return NotFound();
            }
            return registration;
        }

        // Put : api/Registrations/2
        [HttpPut]
        public async Task<ActionResult<Registration>> PutRegistration(int id, Registration registration)
        {
            if (id != registration.RegistrationID)
            {
                return BadRequest();
            }
            _cmsContext.Entry(registration).State = EntityState.Modified;
            try
            {
                await _cmsContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        private bool RegistrationExists(long id)
        {
            return (_cmsContext.Registrations?.Any(registration => registration.RegistrationID == id)).GetValueOrDefault();
        }

        // Post : api/Registrations
        [HttpPost]
        public async Task<ActionResult<Registration>> PostRegistration(Registration registration)
        {
            _cmsContext.Registrations.Add(registration);
            await _cmsContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRegistration), new { id = registration.RegistrationID }, registration);
        }

        // Delete : api/Registrations/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Registration>> DeleteRegistration(int id)
        {
            if (_cmsContext.Registrations is null)
            {
                return NotFound();
            }
            var registration = await _cmsContext.Registrations.FindAsync(id);
            if (registration is null)
            {
                return NotFound();
            }
            _cmsContext.Registrations.Remove(registration);
            await _cmsContext.SaveChangesAsync();
            return NoContent();
        }



    }
}
