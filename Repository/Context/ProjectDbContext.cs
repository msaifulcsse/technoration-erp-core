using Entities.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Context
{
    public partial class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        #region "Database Table Entities"
        public virtual DbSet<CompanySettings> CompanySettings { get; set; }
        public virtual DbSet<MailConfiguration> MailConfigurations { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<AppRole> AppRoles { get; set; }
        public virtual DbSet<AppUserRole> AppUserRoles { get; set; }
        public virtual DbSet<AuthenticationTracer> AuthenticationTracers { get; set; }


        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<StudentBatch> StudentBatches { get; set; }
        public virtual DbSet<Student> Students { get; set; }


        public virtual DbSet<MarkingCriteria> MarkingCriterias { get; set; }
        public virtual DbSet<MarkingBadge> MarkingBadges { get; set; }
        public virtual DbSet<MarkingCriteriasBadge> MarkingCriteriasBadges { get; set; }
        public virtual DbSet<StudentCourseMarks> StudentCourseMarks { get; set; }


        public virtual DbSet<Product> Products { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUserRole>().HasKey(x => new { x.UserId, x.RoleId });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
