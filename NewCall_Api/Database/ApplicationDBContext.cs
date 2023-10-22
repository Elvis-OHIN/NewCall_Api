﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NewCall_Api.Models;

namespace NewCall_Api.Database
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Students> Students { get; set; }

        public DbSet<Absences> Absences { get; set; }

        public DbSet<Admins> Admins { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=call.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Absences>()
                .HasOne(a => a.student)
                .WithMany(s => s.Absences)
                .HasForeignKey(a => a.studentId);
        
        }

    }
}
