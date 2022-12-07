using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;

namespace WebTimeSheetManagement.Controllers
{
    [ValidateSuperAdminSession]
    public class AllRolesController : Controller
    {
        IAssignRoles _IAssignRoles;
        public AllRolesController()
        {
            _IAssignRoles = new AssignRolesConcrete();
        }

        // GET: AllRoles
        public ActionResult Roles()
        {
            return View();
        }

        public ActionResult LoadRolesData()
        {
            try
            {
                var projectdata = _IAssignRoles.ShowallRoles("Name", "desc", "");
                return Json(new { rows = projectdata.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult RemovefromRole(string RegistrationID)
        {
            try
            {
                if (string.IsNullOrEmpty(RegistrationID))
                {
                    return RedirectToAction("Roles");
                }

                var role = _IAssignRoles.RemovefromUserRole(RegistrationID);
                return Json(role);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }


    }
}