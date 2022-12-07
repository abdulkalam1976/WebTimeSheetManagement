

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
    public class TaskassignedController : Controller
    {
        ITaskassigned _ITaskassigned;
        IUsers _IUsers;
        public TaskassignedController()
        {
            _ITaskassigned = new TaskassignedConcrete();
            _IUsers = new UsersConcrete();
        }

        // GET: Task
        public ActionResult Add(int? TaskID)
        {
            try
            {
                if (TaskID == null)
                {

                }
                ViewBag.ModelHeading = "Assigned Employee";
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
                //ViewBag.EmployeeID = _IUsers.ShowallEmployeeUsers();
                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult ListofUsers()
        {
            try
            {
                var listofUsers = _IUsers.ShowallEmployeeUsers();
                return Json(listofUsers, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult LoadTaskAssignedData(int TaskID)
        {
            var data = _ITaskassigned.ShowAssignedEmployeeForTasks(TaskID);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Add(TaskAssigned TaskAssigned)
        {
            if (ModelState.IsValid)
            {
                var result = _ITaskassigned.SaveAssignedTask(TaskAssigned);
                if (result > 0)
                {
                    TempData["EmployeeAssignedMessage"] = "Employee Assigned";
                    return Json(new {ID= result });
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: Taskmaster
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult LoadAssignedEmployeeForATask(int taskid)
        {
            var assigneddata = _ITaskassigned.ShowAssignedEmployeeForTasks(taskid);
            var data = assigneddata.ToList();
            return Json(new { rows = data }, JsonRequestBehavior.AllowGet);
        }

  
        public JsonResult Delete(string AssignedID, int TaskID, int UserID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(AssignedID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var isExistsBurnHour = _ITaskassigned.CheckBurnHourExistsForEmployeeInATask(TaskID, UserID);

                if (isExistsBurnHour == false)
                {
                    var data = _ITaskassigned.TaskAssignedDelete(Convert.ToInt32(AssignedID));

                    if (data > 0)
                    {
                        return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
                    }
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