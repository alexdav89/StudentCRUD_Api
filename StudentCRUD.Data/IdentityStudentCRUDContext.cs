using StudentCRUD.Business.Entities.DomainModels;
using Core.Common.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCRUD.Data {
    public class StudentCRUDContext : IdentityDbContext<User> {
        public StudentCRUDContext(DbContextOptions<StudentCRUDContext> options)
            : base(options) {
        }        


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
    }
}

