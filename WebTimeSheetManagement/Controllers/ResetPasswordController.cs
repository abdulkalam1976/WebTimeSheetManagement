using EventApplicationCore.Library;
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
    public class ResetPasswordController : Controller
    {
        IRegistration _IRegistration;
        public ResetPasswordController()
        {
            _IRegistration = new RegistrationConcrete();
        }

        // GET: ResetPassword
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadRegisteredUserData()
        {
            try
            {
                var usersdata = _IRegistration.ListofRegisteredUser("Name", "desc", "");
                return Json(new { rows = usersdata.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public JsonResult ResetUserPasswordProcess(string RegistrationID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(RegistrationID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var Password = EncryptionLibrary.EncryptText("default@123");
                var isPasswordUpdated = _IRegistration.UpdatePassword(RegistrationID, Password);

                if (isPasswordUpdated)
                {
                    return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}