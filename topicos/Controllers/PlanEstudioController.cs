using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using topicos.data;

namespace topicos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanEstudioController : ControllerBase

    {
        private readonly ApplicationDbContext _context;

        public PlanEstudioController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        // GET api/planDeEstudio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanDeEstudio>>> GetPlanDeEstudio()
        {
            // Lazy Loading: Carga los Planes de Estudio con sus Materias y Prerequisitos
            var planesDeEstudio = await _context.PlanesDeEstudio.ToListAsync();

            return await _context.PlanesDeEstudio.ToListAsync();
          
        }
        // GET api/PlanEstudio/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanDeEstudio>> GetPlanDeEstudio(int id)
        {
            var planDeEstudio = await _context.PlanesDeEstudio
                                               .FirstOrDefaultAsync(p => p.Id == id);

            if (planDeEstudio == null)
            {
                return NotFound();
            }

            return Ok(planDeEstudio);
        }

        // POST api/PlanEstudio
        [HttpPost]
        public async Task<ActionResult<PlanDeEstudio>> PostPlanDeEstudio(PlanDeEstudio planDeEstudio)
        {
            _context.PlanesDeEstudio.Add(planDeEstudio);
            await _context.SaveChangesAsync();

            // Devuelve el PlanDeEstudio recién creado
            return CreatedAtAction(nameof(GetPlanDeEstudio), new { id = planDeEstudio.Id }, planDeEstudio);
        }
        // PUT api/PlanEstudio/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanDeEstudio(int id, PlanDeEstudio planDeEstudio)
        {
            if (id != planDeEstudio.Id)
            {
                return BadRequest();
            }

            _context.Entry(planDeEstudio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanDeEstudioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();  // Devuelve 204 si la actualización es exitosa
        }
        // DELETE api/PlanEstudio/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanDeEstudio(int id)
        {
            var planDeEstudio = await _context.PlanesDeEstudio.FindAsync(id);
            if (planDeEstudio == null)
            {
                return NotFound();
            }

            _context.PlanesDeEstudio.Remove(planDeEstudio);
            await _context.SaveChangesAsync();

            return NoContent();  // Devuelve 204 si la eliminación es exitosa
        }

        private bool PlanDeEstudioExists(int id)
        {
            return _context.PlanesDeEstudio.Any(e => e.Id == id);
        }
    }
}
