using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PatientsManagement.Common.Models;
using PatientsManagement.Storage;

namespace PatientsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        public PatientsController(PatientsManagementContext context)
        {
            this.context = context;
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            context.Entry(patient).State = EntityState.Modified;

            // TODO: modify index?

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<int>> PostPatient(Patient patient)
        {
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            // TODO: add to index?

            return patient.Id;
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            var patient = await context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            context.Patients.Remove(patient);
            await context.SaveChangesAsync();

            // TODO: remove from index?

            return patient;
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Patient>>> Search([FromQuery] string searchString)
        {
            // TODO: ask in the index
            return new List<Patient>() { new Patient() };
        }

        bool PatientExists(int id) => context.Patients.Any(e => e.Id == id);

        readonly PatientsManagementContext context;
    }
}
