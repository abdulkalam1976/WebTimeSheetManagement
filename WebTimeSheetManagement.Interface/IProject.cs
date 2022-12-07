using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface IProject
    {
        List<ProjectMaster> GetListofProjects();
        bool CheckProjectCodeExists(string ProjectCode);
        bool CheckProjectNameExists(string ProjectName);
        int SaveProject(ProjectMaster ProjectMaster);
        IQueryable<ProjectMaster> ShowProjects(string sortColumn, string sortColumnDir, string Search);
        int ProjectDelete(int ProjectID);
        int GetTotalProjectsCounts();
        bool CheckProjectIDExistsInTimesheet(int ProjectID);
    }
}
