﻿using Entities.EntityModels;
using Repository.Context;
using Repository.utility;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Repository.Migrations
{
    public static class DBConfiguration
    {
        //After Seeding Default Data: This Block Code Must Be Commented
        public static void SeedDefaultRoleUserAndUserRoleData(ProjectDbContext dbContext)
        {
            try
            {
                //dbContext.Database.EnsureDeleted();
                //dbContext.Database.EnsureCreated();
                //DefaultUsersDataSeed(dbContext);
            }
            catch (Exception ex) { }
        }

        public static void DefaultUsersDataSeed(ProjectDbContext dbContext)
        {
            if (!dbContext.AppRoles.Any())
            {
                var roles = new AppRole[]
                {
                        new AppRole { RoleId = 1, RoleName = ApplicationRoles.SuperAdmin.ToString(), CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true },
                        new AppRole { RoleId = 2, RoleName = ApplicationRoles.AppAdmin.ToString(), CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true },
                        new AppRole { RoleId = 3, RoleName = ApplicationRoles.Employee.ToString(), CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true },
                        new AppRole { RoleId = 4, RoleName = ApplicationRoles.Stundent.ToString(), CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true }
                };
                dbContext.AppRoles.AddRange(roles);
                dbContext.SaveChanges();
            }
            if (!dbContext.AppUsers.Any())
            {
                //password => Q890qWhIvIKy3LTPAnXfGxOVAqbARieqikQoDuWzQZ4=
                var users = new AppUser[]
                {
                        new AppUser { UserId = 1, UserName = "superadmin", Password = "Q890qWhIvIKy3LTPAnXfGxOVAqbARieqikQoDuWzQZ4=", UserType = (int)ApplicationRoles.SuperAdmin, ReferenceId = 0, CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true },
                        new AppUser { UserId = 2, UserName = "spaadmin", Password = "Q890qWhIvIKy3LTPAnXfGxOVAqbARieqikQoDuWzQZ4=", UserType = (int)ApplicationRoles.AppAdmin, ReferenceId = 0, CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true },
                        new AppUser { UserId = 3, UserName = "mremployee", Password = "Q890qWhIvIKy3LTPAnXfGxOVAqbARieqikQoDuWzQZ4=", UserType = (int)ApplicationRoles.Employee, ReferenceId = 0, CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true },
                        new AppUser { UserId = 4, UserName = "mariful", Password = "Q890qWhIvIKy3LTPAnXfGxOVAqbARieqikQoDuWzQZ4=", UserType = (int)ApplicationRoles.Stundent, ReferenceId = 0, CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true }
                };
                dbContext.AppUsers.AddRange(users);
                dbContext.SaveChanges();
            }
            if (!dbContext.AppUserRoles.Any())
            {
                var userRoles = new AppUserRole[]
                {
                    new AppUserRole { RoleId = 1, UserId = 1, CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true },
                    new AppUserRole { RoleId = 2, UserId = 2, CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true },
                    new AppUserRole { RoleId = 3, UserId = 3, CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true },
                    new AppUserRole { RoleId = 4, UserId = 4, CreatedBy = 1, CreatedDate = DateTime.Now, UpdatedBy = 1, UpdatedDate = DateTime.Now, IsActive = true }
                };
                dbContext.AppUserRoles.AddRange(userRoles);
                dbContext.SaveChanges();
            }
        }
    }
}