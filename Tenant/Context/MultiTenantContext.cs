using Entities.TenantModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tenant.Context
{
    public partial class MultiTenantContext : DbContext
    {
        public MultiTenantContext()
        {
        }

        public MultiTenantContext(DbContextOptions<MultiTenantContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=saiful-pc;password=hellobd789;database=spa-multitenant-db", x => x.ServerVersion("10.4.11-mariadb"));
            }
        }


        public virtual DbSet<Multitenant> Multitenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
