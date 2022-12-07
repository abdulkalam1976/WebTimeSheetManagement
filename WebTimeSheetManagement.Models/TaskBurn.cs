using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace WebTimeSheetManagement.Models
{


    [Table("TaskBurn")]
    public  class TaskBurn
    {
        public TaskBurn()
        {
            Isdeleted = false;
            Createddate = DateTime.Now;
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int ID { get; set; }

        public int Taskid { get; set; }

        public int Userid { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Burnhour { get; set; }

        [Column(TypeName = "date")]
        public DateTime Burndate { get; set; }

        public bool Isdeleted { get; set; }

        public DateTime Createddate { get; set; }

        public int Createdby { get; set; }

        public virtual TaskMaster TaskMaster { get; set; }
    }
}
