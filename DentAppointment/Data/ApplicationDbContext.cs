using System;
using System.Collections.Generic;
using System.Text;
using DentAppointment.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DentAppointment.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // match my db tables with my classes
        public DbSet<Appointment> Appointments { get; set; }
    }
}
