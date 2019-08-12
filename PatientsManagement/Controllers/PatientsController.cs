using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Nest;

using PatientsManagement.Common.Models;
using PatientsManagement.Elastic;
using PatientsManagement.Storage;

namespace PatientsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        public PatientsController(
            PatientsManagementContext context,
            IElasticClient elastic,
            IQueryService queryService)
        {
            this.context = context;
            this.elastic = elastic;
            this.queryService = queryService;
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await context.Patients.FindAsync(id);

            if (patient == null || !patient.IsActive)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            patient.IsActive = true;
            if (id != patient.Id)
            {
                return BadRequest();
            }

            try
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    // TODO: check if patient is active!
                    context.Entry(patient).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    var elasticResult = await elastic.UpdateAsync<Patient>(patient, u => u.Doc(patient));
                    if (!elasticResult.IsValid)
                        throw new InvalidOperationException("couldn't update index");
                    transaction.Commit();
                }
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
            patient.IsActive = true;
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                context.Patients.Add(patient);
                await context.SaveChangesAsync();

                var elasticResult = await elastic.IndexDocumentAsync(patient);
                if (!elasticResult.IsValid)
                    throw new InvalidOperationException("couldn't update index");

                transaction.Commit();
            }

            return patient.Id;
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            var patient = await context.Patients.FindAsync(id);
            if (patient == null || !patient.IsActive)
            {
                return NotFound();
            }

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                patient.IsActive = false;
                await context.SaveChangesAsync();

                var elasticResult = await elastic.DeleteAsync<Patient>(patient);
                if (!elasticResult.IsValid)
                    throw new InvalidOperationException("couldn't update index");

                transaction.Commit();
            }

            return patient;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IReadOnlyCollection<Patient>>> Search(
            [FromQuery] [StringLength(256, MinimumLength = 3)] string queryString)
        {
            if (queryString == null)
                return BadRequest("No query specified");
            var searchResult = await elastic.SearchAsync<Patient>(
                s => queryService.PrepareQuery(s, queryString));
            return Ok(searchResult.Documents);
        }

        bool PatientExists(int id)
        {
            var p = context.Patients.FirstOrDefault(e => e.Id == id);
            return p != null && p.IsActive;
        }

        readonly PatientsManagementContext context;
        readonly IElasticClient elastic;
        readonly IQueryService queryService;
    }
}
