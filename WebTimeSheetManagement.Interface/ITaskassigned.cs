using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface ITaskassigned
    {
        int SaveAssignedTask(TaskAssigned TaskMaster);
        List<TaskAssignedViewModel> ShowAssignedEmployeeForTasks(int Taskid);
        int TaskAssignedDelete(int AssigneID);

        bool CheckBurnHourExistsForEmployeeInATask(int TaskID, int UserID);
    }
}
