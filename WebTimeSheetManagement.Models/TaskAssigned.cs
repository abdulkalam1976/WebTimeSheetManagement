using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace WebTimeSheetManagement.Models
{


    [Table("TaskAssigned")]
    public  class TaskAssigned
    {
        public TaskAssigned()
        {
            Assigneddate = DateTime.Now;
        }

        [Key]
        public int TaskAssignedID { get; set; }

        public int Taskid { get; set; }

        public int Userid { get; set; }

        public DateTime Assigneddate { get; set; }

        public int Assignedby { get; set; }

        public virtual TaskMaster TaskMaster { get; set; }
    }

    public class TaskAssignedViewModel
    {
        public int TaskAssignedID { get; set; }
        public int Taskid { get; set; }
        public int Userid { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Assigneddate { get; set; }

        public int Assignedby { get; set; }
        public string AssignedByName { get; set; }
    }
}
