using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    [NotMapped]
    public class TimeSheetMasterView
    {
        public int TimeSheetID { get; set; }
        [Display(Name = "Burn Date")]
        public string Period { get; set; }
        [Display(Name = "Total Hours")]
        public decimal TotalHours { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Created On")]
        public string CreatedOn { get; set; }
        [Display(Name = "Employee")]
        public string Username { get; set; }
        public string SubmittedMonth { get; set; }
        [Display(Name = "Status")]
        public string TimeSheetStatus { get; set; }
        public string Comment { get; set; }
        public int TaskID { get; set; }
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }
        [Display(Name = "Action By")]
        public string ActionTakenBy { get; set; }
        [Display(Name = "Action On")]
        public string ActionTakenOn { get; set; }
        [Display(Name = "Action Remarks")]
        public string ActionTakenRemarks { get; set; }
        [Display(Name = "Est. Hours")]
        public decimal EstimatedHours { get; set; }
    }
}
