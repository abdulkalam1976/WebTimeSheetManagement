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
    public class TaskmasterConcrete : ITaskmaster
    {
        public bool CheckTaskIDBurnHourExists(int TaskID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from taskassign in _context.TimeSheetDetails
                                  where taskassign.TaskID == TaskID && taskassign.Status == 2
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

        public List<TaskMaster> GetListofTasks()
        {
            throw new NotImplementedException();
        }

        public int GetTotalTasksCounts()
        {
            throw new NotImplementedException();
        }

        public int SaveTask(TaskMaster TaskMaster)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.TaskMasters.Add(TaskMaster);
                    return _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateTask(TaskMaster tm)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.Entry(tm).State = System.Data.Entity.EntityState.Modified;
                    int resultProject = _context.SaveChanges();
                    return 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TaskMasterModelView> ShowTasks(int UserID, int IsAdmin)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var param = new DynamicParameters();

                param.Add("@UserID", UserID);
                param.Add("@IsAdmin", IsAdmin);
                var IQueryabletasks = con.Query<TaskMasterModelView>("GetTaskData", param, null, true, 0, System.Data.CommandType.StoredProcedure).AsQueryable();
                return IQueryabletasks.ToList();
            }

            //Status = (
            //     taskmaster.Status == "A" ? "Assigned" :
            //     taskmaster.Status == "I" ? "Inprogres" :
            //     taskmaster.Status == "H" ? "Hold" :
            //     taskmaster.Status == "C" ? "Closed" : "Unkonown"
            //     ),

        }

        public int TaskDelete(int TaskID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                        TaskMaster tm = _context.TaskMasters.Find(TaskID);
                        tm.Isdeleted = true;
                        _context.Entry(tm).State = System.Data.Entity.EntityState.Modified;
                        int resultProject = _context.SaveChanges();
                        return 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public TaskMasterModelView GetTaskModelDetails(int TaskID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {

                    var IQueryabletasks = (from taskmaster in _context.TaskMasters
                                           join r1 in _context.Registration on taskmaster.Createdby equals r1.RegistrationID
                                           join p in _context.ProjectMaster on taskmaster.Projectid equals p.ProjectID
                                           join st in _context.Enumerations on taskmaster.Status equals st.Enumid
                                           join pr in _context.Enumerations on taskmaster.Priority equals pr.Enumid
                                           join co in _context.Enumerations on taskmaster.Complexity equals co.Enumid
                                           where taskmaster.TaskID== TaskID
                                           select new TaskMasterModelView
                                           {
                                               TaskID = taskmaster.TaskID,
                                               Projectid = taskmaster.Projectid,
                                               ProjectName = p.ProjectName,
                                               Title = taskmaster.Title,
                                               Description = taskmaster.Description,
                                               Scheduledstartdate = taskmaster.Scheduledstartdate,
                                               Scheduledenddate = taskmaster.Scheduledenddate,
                                               Actualstartdate = taskmaster.Actualstartdate,
                                               Actualenddate = taskmaster.Actualenddate,
                                               Status = st.Enumdesc,
                                               Priority = pr.Enumdesc,
                                               Complexity = co.Enumdesc,
                                               EstimatedHour = taskmaster.EstimatedHour,
                                               Isactive = taskmaster.Isactive,
                                               Isdeleted = taskmaster.Isdeleted,
                                               Createddate = taskmaster.Createddate,
                                               Createdby = taskmaster.Createdby,
                                               CreatedbyName = r1.Name
                                           }
                                           );

                    return IQueryabletasks.SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public TaskMaster GetTaskDetails(int TaskID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {

                    TaskMaster tm = _context.TaskMasters.Find(TaskID);
                    return tm;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
