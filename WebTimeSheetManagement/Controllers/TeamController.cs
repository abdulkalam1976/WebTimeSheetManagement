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
    [ValidateAdminSession]
    public class TeamController : Controller
    {
        private IUsers _IUsers;
        public TeamController()
        {
            _IUsers = new UsersConcrete();
        }
        // GET: Team
        public ActionResult Members()
        {
            return View();
        }

        public ActionResult LoadUsersData()
        {
            try
            {
                var adminUserID = Convert.ToInt32(Session["AdminUser"]);
                var projectdata = _IUsers.ShowallUsersUnderAdmin("Username", "desc", "", adminUserID);
                return Json(new { rows = projectdata.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult UserDetails(int? RegistrationID)
        {
            try
            {
                if (RegistrationID == null)
                {

                }
                var userDetailsResponse = _IUsers.GetUserDetailsByRegistrationID(RegistrationID);
                return PartialView("_UserDetails", userDetailsResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}