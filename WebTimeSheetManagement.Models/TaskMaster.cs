using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace WebTimeSheetManagement.Models
{

    [Table("TaskMaster")]
    public class TaskMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaskMaster()
        {
            TaskAssigneds = new HashSet<TaskAssigned>();
            Isactive = true;
            Isdeleted = false;
            Createddate = DateTime.Now;
        }

        [Key]
        public int TaskID { get; set; }

        [Display(Name = "Project")]
        [Required(ErrorMessage = "Choose Project")]
        public int Projectid { get; set; }
        
        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Scheduled Start Date")]
        [DataType(DataType.Date)]
        public DateTime? Scheduledstartdate { get; set; }

        [Display(Name = "Scheduled End Date")]
        [DataType(DataType.Date)]
        public DateTime? Scheduledenddate { get; set; }

        [Display(Name = "Actual Start Date")]
        public DateTime? Actualstartdate { get; set; }

        [Display(Name = "Actual End Date")]
        public DateTime? Actualenddate { get; set; }

        public int Status { get; set; }

        public int Priority { get; set; }

        public int Complexity { get; set; }
                
        [Display(Name = "Estimated Hour")]
        [Column(TypeName = "numeric")]
        [Required]
        public decimal EstimatedHour { get; set; }

        public bool Isactive { get; set; }

        public bool Isdeleted { get; set; }

        public DateTime Createddate { get; set; }

        public int Createdby { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskAssigned> TaskAssigneds { get; set; }


    }

    public class TaskMasterModelView
    {
        public int TaskID { get; set; }
        public int Projectid { get; set; }
        [Display(Name = "Project")]
        public string ProjectName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Scheduled Start Date")]
        public DateTime? Scheduledstartdate { get; set; }
        [Display(Name = "Scheduled End Date")]
        public DateTime? Scheduledenddate { get; set; }

        [Display(Name = "Actual Start Date")]
        public DateTime? Actualstartdate { get; set; }

        [Display(Name = "Actual End Date")]
        public DateTime? Actualenddate { get; set; }

        public string Status { get; set; }

        public string Priority { get; set; }
        public string Complexity { get; set; }
        
        [Display(Name = "Estimated Hour")]
        public decimal EstimatedHour { get; set; }

        public bool Isactive { get; set; }

        public bool Isdeleted { get; set; }

        public DateTime Createddate { get; set; }

        public int Createdby { get; set; }
        [Display(Name = "Created By")]
        public string CreatedbyName { get; set; }

    }
}
