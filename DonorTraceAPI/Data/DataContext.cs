using DonorTraceAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorTraceAPI.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<BloodGroup> BloodGroups { get; set; }
        public DbSet<BloodType> BloodTypes { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<DonorOrgan> DonorOrgans { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<OrganList> OrganLists { get; set; }

        public DbSet<Facility> Facilities { get; set; }
        public DbSet<MedicalOfficer> Officers  { get; set; }      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
