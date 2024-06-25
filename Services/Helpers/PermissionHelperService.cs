using Repository.Context;
using Repository.utility;
using Services.Helpers.Interfaces;
using System;
using System.Linq;

namespace Services.Helpers
{
    public class PermissionHelperService : IPermissionHelperService
    {
        #region "Declared objects & Constructor"
        //private IUnitOfWork unitOfWork;
        //private ApplicationDbContext appDbContext;
        //private Controller controllerContext;

        //public PermissionHelperService(IUnitOfWork unitOfWork, ApplicationDbContext appDbContext, Controller controllerContext)
        //{
        //    this.unitOfWork = unitOfWork;
        //    this.appDbContext = appDbContext;
        //    this.controllerContext = controllerContext;
        //}
        #endregion

        //public bool HasAccessPermission(out int permissionStatus, int? userId, string parentMenuURL = null)
        //{
        //    bool hasPermission = true;
        //    int? permissionId = 0;
        //    if (userId != null && userId > 0)
        //    {
        //        try
        //        {
        //            var controllerName = controllerContext.RouteData.Values["controller"].ToString();
        //            var actionName = controllerContext.RouteData.Values["action"].ToString();
        //            var url = !string.IsNullOrEmpty(parentMenuURL) ? parentMenuURL : ("/" + controllerName + "/" + actionName);
        //            var userRoleId = appDbContext.Users.Find(userId).Roles.Select(s => s.RoleId).FirstOrDefault();
        //            if (userRoleId > 0)
        //            {
        //                var menuIds = unitOfWork.RoleModuleRepo.Find(w => w.RoleId == userRoleId).ToList();
        //                if (menuIds != null)
        //                {

        //                    var requestedMenuId = unitOfWork.MenuRepo.FindWithOutBranch(w => !string.IsNullOrEmpty(w.Url) && w.Url.ToLower() == url.ToLower()).Select(s => s.MenuId).FirstOrDefault();
        //                    var menuInfo = menuIds.FirstOrDefault(f => f.MenuId == requestedMenuId);

        //                    var roleInfo = (from userRoleDB in appDbContext.Users.FirstOrDefault(w => w.Id == userId).Roles
        //                                    join roleDB in appDbContext.Roles on userRoleDB.RoleId equals roleDB.Id
        //                                    select new { RoleName = roleDB.Name.Split('-')[0] }).ToList();
        //                    if (roleInfo.Any(a => a.RoleName == AppRole.SuperAdmin.ToString() || a.RoleName == AppRole.AppAdmin.ToString()))
        //                    {
        //                        if (menuInfo != null)
        //                            permissionId = menuInfo.PermissionId != null ? menuInfo.PermissionId : (int)PermissionStatus.FullPermission;
        //                        else
        //                            permissionId = (int)PermissionStatus.FullPermission;
        //                    }
        //                    else
        //                    {
        //                        if (menuInfo != null)
        //                            permissionId = menuInfo.PermissionId != null ? menuInfo.PermissionId : (int)PermissionStatus.ReadPermission;
        //                        else
        //                            permissionId = (int)PermissionStatus.ReadPermission;
        //                    }
        //                }
        //                else
        //                { permissionId = 0; }
        //            }
        //            else
        //            { permissionId = 0; }
        //        }
        //        catch (Exception ex) { hasPermission = true; permissionId = 0; }
        //    }
        //    permissionStatus = (int)permissionId;
        //    return hasPermission;
        //}

        //public bool HasCreatePermission()
        //{
        //    bool hasPermission = false;
        //    try
        //    {
        //        var numberOfBranch = unitOfWork.CompanyBranchRepo.GetAll().Select(s => s.BranchId).Count();
        //        var comSettings = unitOfWork.CompanySettingsRepo.GetFirstWithOutBranch();
        //        if (comSettings != null)
        //        {
        //            if (!string.IsNullOrEmpty(comSettings.Attribute9))
        //            {
        //                var permittedBranchCreateNum = Encrypt.DecryptString(comSettings.Attribute9);
        //                if (!string.IsNullOrEmpty(permittedBranchCreateNum) && numberOfBranch < Convert.ToInt32(permittedBranchCreateNum))
        //                    hasPermission = true;
        //                else
        //                    hasPermission = false;
        //            }
        //            else
        //            {
        //                hasPermission = false;
        //            }
        //            #region "Old Code For Branch Create Permission"
        //            //if (comSettings.BusinessPackage == (int)AppBusinessPackage.Basic)
        //            //    if (numberOfBranch < 1)
        //            //        hasPermission = true;
        //            //    else
        //            //        hasPermission = false;
        //            //else if (comSettings.BusinessPackage == (int)AppBusinessPackage.Standard)
        //            //    if (numberOfBranch < 2)
        //            //        hasPermission = true;
        //            //    else
        //            //        hasPermission = false;
        //            //else if (comSettings.BusinessPackage == (int)AppBusinessPackage.Premium)
        //            //    if (numberOfBranch < 3)
        //            //        hasPermission = true;
        //            //    else
        //            //        hasPermission = false;
        //            //else if (comSettings.BusinessPackage == (int)AppBusinessPackage.Enterprise)
        //            //    if (numberOfBranch < 1)
        //            //        hasPermission = true;
        //            //    else
        //            //        hasPermission = false;
        //            #endregion
        //        }
        //    }
        //    catch (Exception ex) { hasPermission = false; }
        //    return hasPermission;
        //}
    }
}
