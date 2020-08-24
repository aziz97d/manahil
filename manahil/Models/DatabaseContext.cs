using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options)
        {
        }


        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contractor> FieldWorkers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Thana> Thanas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ManageDrive> ManageDrives { get; set; }

        public DbSet<Cat_Department> Cat_Department { get; set; }
    }
}
