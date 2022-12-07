using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;
namespace WebTimeSheetManagement.Controllers
{
    public class TaskmasterController : Controller
    {
        ITaskmaster _ITaskmaster;
        IProject _IProject;
        IEnumeration _IEnumeration;
        public TaskmasterController()
        {
            _IProject = new ProjectConcrete();
            _ITaskmaster = new TaskmasterConcrete();
            _IEnumeration = new EnumerationConcrete();
        }


        // GET: Task
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskMaster tm = _ITaskmaster.GetTaskDetails(id);
            if (tm== null)
            {
                return HttpNotFound();
            }
            return View(tm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskMaster TaskMaster)
        {
            if (ModelState.IsValid)
            {
                var result = _ITaskmaster.UpdateTask(TaskMaster);

                if (result > 0)
                {
                    TempData["TaskMessage"] = "Task Updated";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
            }

            return View(TaskMaster);
        }


        // GET: Task
        [HttpGet]
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.TaskID = id;

            TaskMasterModelView tm = _ITaskmaster.GetTaskModelDetails(id);
            if (tm == null)
            {
                return HttpNotFound();
            }
            return View(tm);
        }


        // GET: Task
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }


        public JsonResult ListofProjects()
        {
            try
            {
                var listofProjects = _IProject.GetListofProjects();
                return Json(listofProjects, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult ListofEnumerations()
        {
            try
            {
                var listofEnumerations = _IEnumeration.GetListofEnumeration();
                return Json(listofEnumerations, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TaskMaster TaskMaster)
        {
            if (ModelState.IsValid)
            {
                TaskMaster.Createdby = Convert.ToInt32(Session["UserID"]);
                var result = _ITaskmaster.SaveTask(TaskMaster);

                if (result > 0)
                {
                    TempData["TaskMessage"] = "Task Added";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
            }

            return View(TaskMaster);
        }

        // GET: Taskmaster
        public ActionResult Index()
        {
            switch (Convert.ToInt16(Session["RoleID"]))
            {
                case 1:
                    ViewBag.CurrentUser = Convert.ToInt32(Session["AdminUser"]);
                    ViewBag.IsAdmin = 1;
                    break;
                case 2:
                    ViewBag.CurrentUser = Convert.ToInt32(Session["UserID"]);
                    ViewBag.IsAdmin = 0;
                    break;
                case 3:
                    ViewBag.CurrentUser = Convert.ToInt32(Session["SuperAdmin"]);
                    ViewBag.IsAdmin = 1;
                    break;
                default:
                    break;
            }

            return View();
        }


        public ActionResult LoadTaskData()
        {
            int userid=0;
            int isadmin=0;
            switch (Convert.ToInt16(Session["RoleID"]))
            {
                case 1:
                    userid = Convert.ToInt32(Session["AdminUser"]);
                    isadmin = 1;
                    break;
                case 2:
                    userid = Convert.ToInt32(Session["UserID"]);
                    isadmin = 0;
                    break;
                case 3:
                    userid = Convert.ToInt32(Session["SuperAdmin"]);
                    isadmin = 1;
                    break;
                default:
                    break;
            }

            var data = _ITaskmaster.ShowTasks(userid, isadmin);
            return Json(new { rows=data }, JsonRequestBehavior.AllowGet);
        }

         public JsonResult Delete(string TaskID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(TaskID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var isExistsBurnHour = _ITaskmaster.CheckTaskIDBurnHourExists(Convert.ToInt32(TaskID));

                if (isExistsBurnHour == false )
                {
                    var data = _ITaskmaster.TaskDelete(Convert.ToInt32(TaskID));

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