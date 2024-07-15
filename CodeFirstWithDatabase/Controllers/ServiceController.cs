using CodeFirstWithDatabase.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstWithDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly CMSContext _cmsContext;
        public ServiceController(CMSContext CMSContext)
        {
            _cmsContext = CMSContext;
        }

        // GET: api/Service
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            if (_cmsContext.Services == null)
            {
                return NotFound();
            }
            return await _cmsContext.Services.ToListAsync();
        }

        // GET: api/Service/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            if (_cmsContext.Services is null)
            {
                return NotFound();
            }
            var service = await _cmsContext.Services.FindAsync(id);
            if (service is null)
            {
                return NotFound();
            }
            return service;
        }


        // GET: api/Service/Top3Expensive
        [HttpGet("Top3Expensive")]
        public async Task<ActionResult<IEnumerable<Service>>> GetTop3ExpensiveServices()
        {
            if (_cmsContext.Services == null)
            {
                return NotFound();
            }
            var top3Services = await _cmsContext.Services
                .OrderByDescending(s => s.Price)
                .Take(3)
                .ToListAsync();
            return top3Services;
        }


        // GET: api/Service/CheckUniqueServiceName/{serviceName}
        [HttpGet("CheckUniqueServiceName/{serviceName}")]
        public async Task<ActionResult<bool>> CheckUniqueServiceName(string serviceName)
        {
            if (_cmsContext.Services == null)
            {
                return NotFound();
            }

            var exists = await _cmsContext.Services
                .AnyAsync(s => s.ServiceName == serviceName);

            return !exists;
        }


        // Put : api/Services/2
        [HttpPut]
        public async Task<ActionResult<Service>> PutService(int id, Service service)
        {
            if (id != service.ServiceId)
            {
                return BadRequest();
            }
            _cmsContext.Entry(service).State = EntityState.Modified;
            try
            {
                await _cmsContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        private bool ServiceExists(long id)
        {
            return (_cmsContext.Services?.Any(service => service.ServiceId == id)).GetValueOrDefault();
        }

        // Post : api/Services
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            _cmsContext.Services.Add(service);
            await _cmsContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetService), new { id = service.ServiceId }, service);
        }

        // Delete : api/Services/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Service>> DeleteService(int id)
        {
            if (_cmsContext.Services is null)
            {
                return NotFound();
            }
            var service = await _cmsContext.Services.FindAsync(id);
            if (service is null)
            {
                return NotFound();
            }
            _cmsContext.Services.Remove(service);
            await _cmsContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
