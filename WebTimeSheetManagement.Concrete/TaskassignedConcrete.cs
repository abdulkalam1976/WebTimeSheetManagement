using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace WebTimeSheetManagement.Concrete
{
    public class TaskassignedConcrete : ITaskassigned
    {
        
        public int SaveAssignedTask(TaskAssigned TaskAssigned)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.TaskAssigneds.Add(TaskAssigned);
                    _context.SaveChanges();
                    return TaskAssigned.TaskAssignedID;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TaskAssignedViewModel> ShowAssignedEmployeeForTasks(int Taskid)
        {
            var _context = new DatabaseContext();

            var IQueryabletasks = (from assigned in _context.TaskAssigneds
                                   join p in _context.Registration on assigned.Userid equals p.RegistrationID
                                   join t in _context.Registration on assigned.Assignedby equals t.RegistrationID
                                   where assigned.Taskid== Taskid
                                   select new TaskAssignedViewModel
                                   {
                                        TaskAssignedID = assigned.TaskAssignedID,
                                        Taskid = assigned.Taskid,
                                        Userid = assigned.Userid,
                                        UserName = p.Name,
                                        Assigneddate = assigned.Assigneddate,
                                        Assignedby = assigned.Assignedby,
                                        AssignedByName = t.Name
                                   }
                               );

            return IQueryabletasks.ToList();
        }

        public int TaskAssignedDelete(int AssignedID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var taskassign = (from t in _context.TaskAssigneds
                                   where t.TaskAssignedID == AssignedID
                                      select t).SingleOrDefault(); ;

                    if (taskassign != null)
                    {
                        _context.TaskAssigneds.Remove(taskassign);
                        int resultTaskAssigneds = _context.SaveChanges();
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

        public bool CheckBurnHourExistsForEmployeeInATask(int TaskID, int UserID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from taskassign in _context.TimeSheetDetails
                                  where taskassign.TaskID == TaskID && taskassign.UserID == UserID 
                                  select taskassign).Count();

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

    }
}
