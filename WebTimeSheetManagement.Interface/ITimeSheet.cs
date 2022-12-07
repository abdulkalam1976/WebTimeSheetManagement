using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface ITimeSheet
    {

        bool UpdateTimeSheetStatus(TimeSheetApproval timesheetapprovalmodel, int Status);
        int AddTimeSheetDetail(TimeSheetDetails TimeSheetDetails);
        bool CheckIsDateAlreadyUsed(DateTime FromDate, int UserID);
        IQueryable<TimeSheetMasterView> ShowTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID);
        IQueryable<TimeSheetMasterView> ShowAllTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID);
        List<TimeSheetMasterView> TimesheetDetailsbyTaskID(int TaskID);
        List<GetPeriods> GetPeriodsbyTimeSheetMasterID(int TimeSheetMasterID);
        void InsertTimeSheetAuditLog(TimeSheetAuditTB timesheetaudittb);
        int? InsertDescription(DescriptionTB DescriptionTB);
        DisplayViewModel GetTimeSheetsCountByAdminID(string AdminID);
        IQueryable<TimeSheetMasterView> ShowAllApprovedTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID);
        IQueryable<TimeSheetMasterView> ShowAllRejectTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID);
        IQueryable<TimeSheetMasterView> ShowAllSubmittedTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID);
        DisplayViewModel GetTimeSheetsCountByUserID(string UserID);
        IQueryable<TimeSheetMasterView> ShowTimeSheetStatus(string sortColumn, string sortColumnDir, string Search, int UserID, int TimeSheetStatus);
        bool UpdateTimeSheetAuditStatus(int TimeSheetID, string Comment, int Status);
        bool IsTimesheetALreadyProcessed(int TimeSheetID);
        int DeleteTimesheetByTimeSheetID(int TimeSheetID);

        TimeSheetMasterView TimesheetDetailsbyTimesheetID(int TimeSheetID);
    }
}
