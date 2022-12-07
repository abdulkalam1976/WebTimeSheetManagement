using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;
using System.Linq.Dynamic;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace WebTimeSheetManagement.Concrete
{
    public class TimeSheetConcrete : ITimeSheet
    {
  
        public int AddTimeSheetDetail(TimeSheetDetails TimeSheetDetails)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.TimeSheetDetails.Add(TimeSheetDetails);
                    _context.SaveChanges();
                    int id = TimeSheetDetails.TimeSheetID; 
                    return id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckIsDateAlreadyUsed(DateTime FromDate, int UserID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from timesheetdetails in _context.TimeSheetDetails
                                  where timesheetdetails.Period == FromDate && timesheetdetails.UserID == UserID
                                  select timesheetdetails).Count();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TimeSheetMasterView> ShowTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var IQueryabletimesheet = ReturnTimesheetStatusData(UserID, 0);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.Period == Search || m.Username == Search);
            }

            return IQueryabletimesheet;
        }

        public List<TimeSheetMasterView> TimesheetDetailsbyTaskID(int TaskID)
        {
            var IQueryabletimesheet = ReturnTimesheetStatusData(0, 0, 0, 0, TaskID);
            return IQueryabletimesheet.ToList();
        }

    
        public IQueryable<TimeSheetMasterView> ShowAllTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var IQueryabletimesheet = ReturnTimesheetStatusData(UserID, 0,1,0,0);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.Period == Search || m.Username == Search || m.TimeSheetStatus == Search);
            }

            return IQueryabletimesheet;
            
        }


        public bool UpdateTimeSheetStatus(TimeSheetApproval timesheetapprovalmodel, int Status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@TimeSheetID", timesheetapprovalmodel.TimeSheetID);
                    param.Add("@Comment", timesheetapprovalmodel.Comment);
                    param.Add("@TimeSheetStatus", Status);
                    var result = con.Execute("Usp_UpdateTimeSheetStatus", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }

        public void InsertTimeSheetAuditLog(TimeSheetAuditTB timesheetaudittb)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.TimeSheetAuditTB.Add(timesheetaudittb);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int? InsertDescription(DescriptionTB DescriptionTB)
        {
            using (var _context = new DatabaseContext())
            {
                _context.DescriptionTB.Add(DescriptionTB);
                _context.SaveChanges();
                int? id = DescriptionTB.DescriptionID; // Yes it's here
                return id;
            }
        }

        public DisplayViewModel GetTimeSheetsCountByAdminID(string AdminID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var param = new DynamicParameters();
                param.Add("@AdminID", AdminID);
                return con.Query<DisplayViewModel>("Usp_GetTimeSheetsCountByAdminID", param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public IQueryable<TimeSheetMasterView> ShowAllApprovedTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var IQueryabletimesheet = ReturnTimesheetStatusData(UserID, 2, 1,0,0);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.Period == Search || m.Username == Search);
            }

            return IQueryabletimesheet;
        }

        public IQueryable<TimeSheetMasterView> ShowAllRejectTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var IQueryabletimesheet = ReturnTimesheetStatusData(UserID, 3, 1,0,0);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.Period == Search || m.Username == Search);
            }

            return IQueryabletimesheet;
            
        }

        public IQueryable<TimeSheetMasterView> ShowAllSubmittedTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var IQueryabletimesheet = ReturnTimesheetStatusData(UserID, 1, 1,0,0);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.Period == Search || m.Username == Search);
            }

            return IQueryabletimesheet;

        }

        //Needed for Approval details Done, used for details of 
        public List<GetPeriods> GetPeriodsbyTimeSheetMasterID(int TimeSheetID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@TimeSheetID", TimeSheetID);
                    var result = con.Query<GetPeriods>("Usp_GetPeriodsbyTimeSheetMasterID", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
                    if (result.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return new List<GetPeriods>();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public DisplayViewModel GetTimeSheetsCountByUserID(string UserID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var param = new DynamicParameters();
                param.Add("@UserID", UserID);
                return con.Query<DisplayViewModel>("Usp_GetTimeSheetsCountByUserID", param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public IQueryable<TimeSheetMasterView> ReturnTimesheetStatusData(int UserID=0, int TimeSheetStatus=0, int IsAdmin=0, int TimeSheetID =0, int TaskID=0)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var param = new DynamicParameters();
                param.Add("@UserID", UserID);
                param.Add("@TimeSheetStatus", TimeSheetStatus);
                param.Add("@IsAdmin", IsAdmin);
                param.Add("@TimeSheetID", TimeSheetID);
                param.Add("@TaskID", TaskID);
                var IQueryabletimesheet = con.Query<TimeSheetMasterView>("GetTimeSheetData", param, null, true, 0, System.Data.CommandType.StoredProcedure).AsQueryable();

                return IQueryabletimesheet;
            }
        }

        public IQueryable<TimeSheetMasterView> ShowTimeSheetStatus(string sortColumn, string sortColumnDir, string Search, int UserID, int TimeSheetStatus)
        {
            var IQueryabletimesheet = ReturnTimesheetStatusData(UserID, TimeSheetStatus,0,0,0);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.Period == Search || m.Username == Search);
            }

            return IQueryabletimesheet;
        }

        public bool UpdateTimeSheetAuditStatus(int TimeSheetID, string Comment, int Status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@TimeSheetID", TimeSheetID);
                    param.Add("@Comment", Comment);
                    param.Add("@Status", Status);
                    var result = con.Execute("Usp_ChangeTimesheetStatus", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }

        public bool IsTimesheetALreadyProcessed(int TimeSheetID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from timesheet in _context.TimeSheetAuditTB
                            where timesheet.TimeSheetID == TimeSheetID && timesheet.Status != 1
                            select timesheet).Count();

                if (data > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Only submitted state of Time shee could be deleted
        public int DeleteTimesheetByTimeSheetID(int TimeSheetID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var TimeSheets = (from timesheet in _context.TimeSheetDetails 
                                   where timesheet.TimeSheetID == TimeSheetID && timesheet.Status==1
                                       select timesheet).SingleOrDefault(); ;

                    if (TimeSheets != null)
                    {
                        _context.TimeSheetDetails.Remove(TimeSheets);
                        int resultProject = _context.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TimeSheetMasterView TimesheetDetailsbyTimesheetID(int TimeSheetID)
        {
            try
            {
                var IQueryabletimesheet = ReturnTimesheetStatusData(0, 0,0,TimeSheetID,0);
                return IQueryabletimesheet.SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
