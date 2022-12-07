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

    public class AllUsersController : Controller
    {
        private IUsers _IUsers;
        public AllUsersController()
        {
            _IUsers = new UsersConcrete();
        }

        // GET: AllUsers
        public ActionResult Users()
        {
            return View();
        }

        public ActionResult LoadUsersData()
        {
            try
            {
                var usersdata = _IUsers.ShowallUsers("Name", "desc", "");
                return Json(new { rows = usersdata.ToList() }, JsonRequestBehavior.AllowGet);
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

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult LoadAdminsData()
        {
            try
            {
                var usersdata = _IUsers.ShowallAdmin("Name", "desc", "");
                return Json(new { rows = usersdata.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult AdminDetails(int? RegistrationID)
        {
            try
            {
                if (RegistrationID == null)
                {

                }
                var userDetailsResponse = _IUsers.GetAdminDetailsByRegistrationID(RegistrationID);
                return PartialView("_UserDetails", userDetailsResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}