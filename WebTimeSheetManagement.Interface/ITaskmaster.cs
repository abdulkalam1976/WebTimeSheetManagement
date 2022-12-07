using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface ITaskmaster
    {
        List<TaskMaster> GetListofTasks();
        int SaveTask(TaskMaster TaskMaster);
        List<TaskMasterModelView> ShowTasks(int UserID, int IsAdmin);
        int TaskDelete(int TaskID);
        int GetTotalTasksCounts();
        bool CheckTaskIDBurnHourExists(int TaskID);
        TaskMaster GetTaskDetails(int TaskID);
        TaskMasterModelView GetTaskModelDetails(int TaskID);
        int UpdateTask(TaskMaster TaskMaster);
    }
}
