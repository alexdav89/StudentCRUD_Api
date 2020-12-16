using StudentCRUD.Business.Entities.DomainModels;
using Core.Common.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCRUD.Data {
    public class StudentCRUDContext : DbContext /*IdentityDbContext<User>*/ {
        public StudentCRUDContext(DbContextOptions<StudentCRUDContext> options)
            : base(options) {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Student> Students { get; set; }

    }
}

