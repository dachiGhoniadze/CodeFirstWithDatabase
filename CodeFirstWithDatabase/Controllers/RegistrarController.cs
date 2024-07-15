using CodeFirstWithDatabase.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstWithDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrarController : ControllerBase
    {
        private readonly CMSContext _cmsContext;
        public RegistrarController(CMSContext CMSContext)
        {
            _cmsContext = CMSContext;
        }

        // GET: api/Registrar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registrar>>> GetRegistrars()
        {
            if (_cmsContext.Registrars == null)
            {
                return NotFound();
            }
            return await _cmsContext.Registrars.ToListAsync();
        }

        // GET: api/Registrar/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Registrar>> GetRegistrar(int id)
        {
            if (_cmsContext.Registrars is null)
            {
                return NotFound();
            }
            var registrar = await _cmsContext.Registrars.FindAsync(id);
            if (registrar is null)
            {
                return NotFound();
            }
            return registrar;
        }

        // Put : api/Registrars/2
        [HttpPut]
        public async Task<ActionResult<Registrar>> PutRegistrar(int id, Registrar registrar)
        {
            if (id != registrar.RegistrarId)
            {
                return BadRequest();
            }
            _cmsContext.Entry(registrar).State = EntityState.Modified;
            try
            {
                await _cmsContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrarExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        private bool RegistrarExists(long id)
        {
            return (_cmsContext.Registrars?.Any(registrar => registrar.RegistrarId == id)).GetValueOrDefault();
        }

        // Post : api/Registrars
        [HttpPost]
        public async Task<ActionResult<Registrar>> PostRegistrar(Registrar registrar)
        {
            _cmsContext.Registrars.Add(registrar);
            await _cmsContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRegistrar), new { id = registrar.RegistrarId }, registrar);
        }

        // Delete : api/Registrars/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Registrar>> DeleteRegistrar(int id)
        {
            if (_cmsContext.Registrars is null)
            {
                return NotFound();
            }
            var registrar = await _cmsContext.Registrars.FindAsync(id);
            if (registrar is null)
            {
                return NotFound();
            }
            _cmsContext.Registrars.Remove(registrar);
            await _cmsContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
