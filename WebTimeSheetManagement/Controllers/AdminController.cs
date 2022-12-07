using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    public class AdminController : Controller
    {
        private ITimeSheet _ITimeSheet;
        
        public AdminController()
        {
            _ITimeSheet = new TimeSheetConcrete();
        }


        [HttpPost]
        public JsonResult DisplayBurnChart(DateTime fromdate, DateTime todate, int? UserID=0)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var param = new DynamicParameters();
                param.Add("@fromdate", fromdate);
                param.Add("@todate", todate);
                param.Add("@UserID", UserID);
                var IQueryabletimesheet = con.Query<BurnHour>("GetEmployeeBurnHour", param, null, true, 0, System.Data.CommandType.StoredProcedure).AsQueryable();

                List<object> iData = new List<object>();

                var a1= IQueryabletimesheet.Select(l => l.UserName).ToList();
                iData.Add(a1);

                var a2 = IQueryabletimesheet.Select(l => l.TotalHours).ToList();
                iData.Add(a2);
                
                return Json(iData, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult DisplayBurnData(DateTime fromdate, DateTime todate, int? UserID = 0)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@fromdate", fromdate);
                    param.Add("@todate", todate);
                    param.Add("@UserID", UserID);
                    var IQueryabletimesheet = con.Query<BurnHour>("GetEmployeeBurnHour", param, null, true, 0, System.Data.CommandType.StoredProcedure).AsQueryable();

                    return Json(new { rows = IQueryabletimesheet.ToList() }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult DisplayBurnHourAgainstEstimated(DateTime fromdate, DateTime todate, int? UserID = 0)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@fromdate", fromdate);
                    param.Add("@todate", todate);
                    param.Add("@UserID", UserID);
                    var IQueryabletimesheet = con.Query<BurnHour>("GetEmployeeBurnHourAgainstEstimated", param, null, true, 0, System.Data.CommandType.StoredProcedure).AsQueryable();

                    return Json(new { rows = IQueryabletimesheet.ToList() }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        


        // GET: Admin
        [HttpGet]
        public ActionResult Dashboard()
        {
            try
            {
                var timesheetResult = _ITimeSheet.GetTimeSheetsCountByAdminID(Convert.ToString(Session["AdminUser"]));

                if (timesheetResult != null)
                {
                    ViewBag.SubmittedTimesheetCount = timesheetResult.SubmittedCount;
                    ViewBag.ApprovedTimesheetCount = timesheetResult.ApprovedCount;
                    ViewBag.RejectedTimesheetCount = timesheetResult.RejectedCount;
                }
                else
                {
                    ViewBag.SubmittedTimesheetCount = 0;
                    ViewBag.ApprovedTimesheetCount = 0;
                    ViewBag.RejectedTimesheetCount = 0;
                }


                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}