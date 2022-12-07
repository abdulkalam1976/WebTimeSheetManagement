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
    [ValidateAdminSession]
    public class ShowAllTimeSheetController : Controller
    {

        IProject _IProject;
        IUsers _IUsers;
        ITimeSheet _ITimeSheet;
        public ShowAllTimeSheetController()
        {
            _IProject = new ProjectConcrete();
            _ITimeSheet = new TimeSheetConcrete();
            _IUsers = new UsersConcrete();
        }

        // GET: ShowAllTimeSheet
        public ActionResult TimeSheet()
        {
            return View();
        }

        public ActionResult LoadTimeSheetData()
        {
            try
            {
                var timesheetdata = _ITimeSheet.ShowAllTimeSheet("TimeSheetID", "desc", "", Convert.ToInt32(Session["AdminUser"]));
                return Json(new { rows = timesheetdata.ToList() }, JsonRequestBehavior.AllowGet);
               }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult Details(int id)
        {
            try
            {
                if (id==0)
                {
                    return RedirectToAction("TimeSheet", "AllTimeSheet");
                }

                TimeSheetMasterView tm = _ITimeSheet.TimesheetDetailsbyTimesheetID(id);
                if (tm == null)
                {
                    return HttpNotFound();
                }

                return View(tm);
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

        public ActionResult Approval(TimeSheetApproval TimeSheetApproval)
        {
            try
            {
                if (TimeSheetApproval.Comment == null)
                {
                    return Json(false);
                }

                if (string.IsNullOrEmpty(Convert.ToString(TimeSheetApproval.TimeSheetID)))
                {
                    return Json(false);
                }

                _ITimeSheet.UpdateTimeSheetStatus(TimeSheetApproval, 2); //Approve

                if (_ITimeSheet.IsTimesheetALreadyProcessed(TimeSheetApproval.TimeSheetID))
                {
                    _ITimeSheet.UpdateTimeSheetAuditStatus(TimeSheetApproval.TimeSheetID, TimeSheetApproval.Comment, 2);
                }
                else
                {
                    _ITimeSheet.InsertTimeSheetAuditLog(InsertTimeSheetAudit(TimeSheetApproval, 2));
                }


                return Json(true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Rejected(TimeSheetApproval TimeSheetApproval)
        {
            try
            {
                if (TimeSheetApproval.Comment == null)
                {
                    return Json(false);
                }

                if (string.IsNullOrEmpty(Convert.ToString(TimeSheetApproval.TimeSheetID)))
                {
                    return Json(false);
                }

                _ITimeSheet.UpdateTimeSheetStatus(TimeSheetApproval, 3); //Reject

                if (_ITimeSheet.IsTimesheetALreadyProcessed(TimeSheetApproval.TimeSheetID))
                {
                    _ITimeSheet.UpdateTimeSheetAuditStatus(TimeSheetApproval.TimeSheetID, TimeSheetApproval.Comment, 3);
                }
                else
                {
                    _ITimeSheet.InsertTimeSheetAuditLog(InsertTimeSheetAudit(TimeSheetApproval, 3));
                }


                return Json(true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private TimeSheetAuditTB InsertTimeSheetAudit(TimeSheetApproval TimeSheetApproval, int Status)
        {
            try
            {
                TimeSheetAuditTB objAuditTB = new TimeSheetAuditTB();
                objAuditTB.ApprovalTimeSheetLogID = 0;
                objAuditTB.TimeSheetID = TimeSheetApproval.TimeSheetID;
                objAuditTB.Status = Status;
                objAuditTB.CreatedOn = DateTime.Now;
                objAuditTB.Comment = TimeSheetApproval.Comment;
                objAuditTB.ApprovalUser = Convert.ToInt32(Session["AdminUser"]);
                objAuditTB.ProcessedDate = DateTime.Now;
                objAuditTB.UserID = _IUsers.GetUserIDbyTimesheetID(TimeSheetApproval.TimeSheetID);
                return objAuditTB;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public JsonResult Delete(int TimeSheetMasterID)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(Convert.ToString(TimeSheetMasterID)))
        //        {
        //            return Json("Error", JsonRequestBehavior.AllowGet);
        //        }

        //        var data = _ITimeSheet.DeleteTimesheetByOnlyTimeSheetMasterID(TimeSheetMasterID);

        //        if (data > 0)
        //        {
        //            return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


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

        public ActionResult LoadSubmittedTData()
        {
            try
            {
                var timesheetdata = _ITimeSheet.ShowAllSubmittedTimeSheet("TimeSheetID", "desc", "", Convert.ToInt32(Session["AdminUser"]));
                return Json(new { rows = timesheetdata.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult LoadRejectedData()
        {
            try
            {
                var timesheetdata = _ITimeSheet.ShowAllRejectTimeSheet("TimeSheetID", "desc", "", Convert.ToInt32(Session["AdminUser"]));
                return Json(new { rows = timesheetdata.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult LoadApprovedData()
        {
            try
            {
                var timesheetdata = _ITimeSheet.ShowAllApprovedTimeSheet("TimeSheetID", "desc", "", Convert.ToInt32(Session["AdminUser"]));
                return Json(new { rows = timesheetdata.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}