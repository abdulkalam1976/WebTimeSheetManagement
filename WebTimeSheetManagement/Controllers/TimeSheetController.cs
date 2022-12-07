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
    public class TimeSheetController : Controller
    {
        ITimeSheet _ITimeSheet;
        public TimeSheetController()
        {
            _ITimeSheet = new TimeSheetConcrete();
        }

        // GET: Task
        public ActionResult Add(int? TaskID)
        {
            try
            {
                if (TaskID == null)
                {

                }
                ViewBag.ModelHeading = "Timesheet Details";
                ViewBag.TaskID = TaskID;

                switch (Convert.ToInt16(Session["RoleID"]))
                {
                    case 1:
                        ViewBag.CurrentUser = Convert.ToInt32(Session["AdminUser"]);
                        break;
                    case 2:
                        ViewBag.CurrentUser = Convert.ToInt32(Session["UserID"]);
                        break;
                    case 3:
                        ViewBag.CurrentUser = Convert.ToInt32(Session["SuperAdmin"]);
                        break;
                    default:
                        break;
                }
                ViewBag.CurrentUserName = @Session["Username"];
                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Add(TimeSheetDetails TimeSheet)
        {
            if (ModelState.IsValid)
            {
                int TimeSheetID = _ITimeSheet.AddTimeSheetDetail(TimeSheet);
                if (TimeSheetID > 0)
                {
                    //Save(timesheetmodel, TimeSheetMasterID);
                    //SaveDescription(timesheetmodel, TimeSheetMasterID);
                    _ITimeSheet.InsertTimeSheetAuditLog(InsertTimeSheetAudit(TimeSheetID, 1));
                    TempData["TimeCardMessage"] = "Timesheet data saved";
                    return Json(new { ID = TimeSheetID });
                }

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadTaskBurnData(int TaskID)
        {
            var data = _ITimeSheet.TimesheetDetailsbyTaskID(TaskID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckIsDateAlreadyUsed(DateTime FromDate)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(FromDate)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var data = _ITimeSheet.CheckIsDateAlreadyUsed(FromDate, Convert.ToInt32(Session["UserID"]));
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult Delete(int TimeSheetID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(TimeSheetID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var data = _ITimeSheet.DeleteTimesheetByTimeSheetID(TimeSheetID);

                if (data > 0)
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

        private TimeSheetAuditTB InsertTimeSheetAudit(int TimeSheetID, int Status)
        {
            try
            {
                TimeSheetAuditTB objAuditTB = new TimeSheetAuditTB();
                objAuditTB.TimeSheetID = TimeSheetID;
                objAuditTB.Status = Status;
                objAuditTB.CreatedOn = DateTime.Now;
                objAuditTB.Comment = string.Empty;
                objAuditTB.ApprovalUser = Convert.ToInt32(Session["UserID"]);
                objAuditTB.ProcessedDate = DateTime.Now;
                objAuditTB.UserID = Convert.ToInt32(Session["UserID"]);
                return objAuditTB;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[NonAction]
        //private void InsertDescriptionDetail(int? ProjectID, int TimeSheetMasterID, string Description)
        //{
        //    try
        //    {
        //        DescriptionTB objtimesheetdetails = new DescriptionTB();
        //        objtimesheetdetails.DescriptionID = 0;
        //        objtimesheetdetails.ProjectID = ProjectID;
        //        objtimesheetdetails.UserID = Convert.ToInt32(Session["UserID"]);
        //        objtimesheetdetails.CreatedOn = DateTime.Now;
        //        objtimesheetdetails.TimeSheetMasterID = TimeSheetMasterID;
        //        objtimesheetdetails.Description = Description;
        //        int? TimeSheetID = _ITimeSheet.InsertDescription(objtimesheetdetails);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

    }
}