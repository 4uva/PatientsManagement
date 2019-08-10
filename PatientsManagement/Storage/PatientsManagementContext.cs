using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientsManagement.Common.Models;

namespace PatientsManagement.Storage
{
    public class PatientsManagementContext : DbContext
    {
        public PatientsManagementContext(DbContextOptions<PatientsManagementContext> options)
            : base(options)
        { }

        public DbSet<Patient> Patients { get; set; }
    }
}
