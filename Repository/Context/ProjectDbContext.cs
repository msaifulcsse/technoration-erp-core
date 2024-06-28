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
        #region "Constructors For SPAnalysisDbContext"
        //public ProjectDbContext() :
        //    base("name = projectconnectionstring")
        //{

        //}

        //public ProjectDbContext(string connectionStringName) :
        //    base(connectionStringName)
        //{
        //}

        //public ProjectDbContext(ITenantProvider tenantProvider) :
        //    base(tenantProvider.GetConnectionString())
        //{
        //}
        #endregion
        public ProjectDbContext()
        {
        }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseMySql("server=localhost;port=3306;user=saiful-pc;password=hellobd789;database=diu-spa-db", x => x.ServerVersion("10.4.11-mariadb"));
                Database.Migrate();
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
            #region "Data Seed => Old Way"
            //modelBuilder.Entity<AppRole>().HasData(
            //   new AppRole { RoleId = 1, RoleName = ApplicationRoles.AppAdmin.ToString(), CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true }
            //   );

            //modelBuilder.Entity<AppUser>().HasData(
            //  new AppUser { UserId = 1, UserName = "admin", Password = "NwGuruUz1Qqd2cqt89MgMTyMaJQlhTNVzT0ex17zu90=", UserType = (int)ApplicationRoles.AppAdmin, ReferenceId = 1, CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true }
            //  );

            //modelBuilder.Entity<AppUserRole>().HasData(
            //  new AppUserRole { UserRoleId = 1, RoleId = 1, UserId = 1, CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true }
            //  );
            #endregion
            modelBuilder.Entity<AppUserRole>().HasKey(x => new { x.UserId, x.RoleId });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
