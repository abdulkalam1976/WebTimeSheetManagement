using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Controllers
{
    //[ValidateUserSession]
    public class AllTimeSheetController : Controller
    {
        IProject _IProject;
        ITimeSheet _ITimeSheet;
        public AllTimeSheetController()
        {
            _IProject = new ProjectConcrete();
            _ITimeSheet = new TimeSheetConcrete();
        }


        // GET: AllTimeSheet
        public ActionResult TimeSheet()
        {
            return View();
        }

        public ActionResult LoadTimeSheetData()
        {
            try
            {
                int currentuserid=0;
                switch(Convert.ToInt16(Session["RoleID"]))
                {
                    case 1:
                        currentuserid = Convert.ToInt32(Session["AdminUser"]);
                        break;
                    case 2:
                        currentuserid = Convert.ToInt32(Session["UserID"]);
                        break;
                    case 3:
                        currentuserid = Convert.ToInt32(Session["SuperAdmin"]);
                        break;
                }
                var timesheetdata = _ITimeSheet.ShowTimeSheet("TimeSheetID", "desc", "", currentuserid);
                return Json(new { rows = timesheetdata.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult Details(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("TimeSheet", "AllTimeSheet");
                }
                MainTimeSheetView objMT = new MainTimeSheetView();
                objMT.ListofPeriods = _ITimeSheet.GetPeriodsbyTimeSheetMasterID(Convert.ToInt32(id));
                objMT.ListoDayofWeek = DayofWeek();
                objMT.TimeSheetMasterID = Convert.ToInt32(id);
                return View(objMT);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [NonAction]
        public List<string> DayofWeek()
        {
            List<string> li = new List<string>();
            li.Add("Sunday");
            li.Add("Monday");
            li.Add("Tuesday");
            li.Add("Wednesday");
            li.Add("Thursday");
            li.Add("Friday");
            li.Add("Saturday");
            li.Add("Total");
            return li;
        }


        public ActionResult SubmittedTimeSheet()
        {
            return View();
        }

        public ActionResult ApprovedTimeSheet()
        {
            return View();
        }

        public ActionResult RejectedTimeSheet()
        {
            return View();
        }


        public ActionResult LoadSubmittedTimeSheetData()
        {
            try
            {
                var timesheetdata = _ITimeSheet.ShowTimeSheetStatus("TimeSheetID", "desc", "", Convert.ToInt32(Session["UserID"]),1);
                return Json(new { rows = timesheetdata.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult LoadRejectedTimeSheetData()
        {
            try
            {
                var timesheetdata = _ITimeSheet.ShowTimeSheetStatus("TimeSheetID", "desc", "", Convert.ToInt32(Session["UserID"]), 3);
                return Json(new { rows = timesheetdata.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult LoadApprovedTimeSheetData()
        {
            try
            {
                var timesheetdata = _ITimeSheet.ShowTimeSheetStatus("TimeSheetID", "desc", "", Convert.ToInt32(Session["UserID"]), 2);
                return Json(new { rows = timesheetdata.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}