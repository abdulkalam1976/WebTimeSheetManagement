using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTimeSheetManagement.Models
{
    [Table("TimeSheetDetails")]
    public class TimeSheetDetails
    {
        public TimeSheetDetails()
        {
            CreatedOn = DateTime.Now;
            Status = 1; //1 = "Submitted", 2 = "Approved" , 3= "Rejected",
        }

        [Key]
        public int TimeSheetID { get; set; }

        [Display(Name = "Spent Hour")]
        [Column(TypeName = "numeric")]
        public decimal Hours { get; set; }

        [Display(Name = "Date")]
        public DateTime Period { get; set; }

        public int TaskID { get; set; }

        public int UserID { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Remarks { get; set; }

        public string Comment { get; set; }
        

        public int Status { get; set; }
    }

    public class TimeSheetApproval
    {
        public int TimeSheetID { get; set; }
        public string Comment { get; set; }
    }
}
