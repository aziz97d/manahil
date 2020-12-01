using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manahil.Models
{
    public class DatabaseContext: IdentityDbContext
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
        
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Payment> Payments { get; set; }
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ManageDrive> ManageDrives { get; set; }
        public DbSet<ApprovalProjects> ApprovalProjects { get; set; }
        public DbSet<DepositAccount> DepositAccounts { get; set; }
        public DbSet<TransferAccount> TransferAccounts { get; set; }
        public DbSet<ManahilMonumental> ManahilMonumentals { get; set; }
        public DbSet<BdDonation> BdDonations { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys())) 
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
