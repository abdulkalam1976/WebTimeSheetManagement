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
    //[ValidateAdminSession]
    public class ProjectController : Controller
    {
        IProject _IProject;
        public ProjectController()
        {
            _IProject = new ProjectConcrete();
        }

        // GET: Project
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        public JsonResult CheckProjectCodeExists(string ProjectCode)
        {
            try
            {
                var isProjectCodeExists = false;

                if (ProjectCode != null)
                {
                    isProjectCodeExists = _IProject.CheckProjectCodeExists(ProjectCode);
                }

                if (isProjectCodeExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult CheckProjectNameExists(string ProjectName)
        {
            try
            {
                var isProjectNameExists = false;

                if (ProjectName != null)
                {
                    isProjectNameExists = _IProject.CheckProjectNameExists(ProjectName);
                }

                if (isProjectNameExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                } 
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProjectMaster ProjectMaster)
        {
            if (ModelState.IsValid)
            {
                var result = _IProject.SaveProject(ProjectMaster);

                if (result > 0)
                {
                    //TempData["ProjectMessage"] = "Project Added Successfully";
                    //ModelState.Clear();
                    return RedirectToAction("Index");
                }
            }

            return View(ProjectMaster);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadProjectData()
        {
            try
            {
                var projectdata = _IProject.ShowProjects("ProjectName", "desc", "");
                return Json(new { rows = projectdata.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public JsonResult Delete(string ProjectID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(ProjectID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var isExistsinTimesheet = _IProject.CheckProjectIDExistsInTimesheet(Convert.ToInt32(ProjectID));

                if (isExistsinTimesheet == false )
                {
                    var data = _IProject.ProjectDelete(Convert.ToInt32(ProjectID));

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