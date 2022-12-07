using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    public class TimeSheetDetailsView
    {

        public int TimeSheetID { get; set; }

        public decimal Hours { get; set; }

        public DateTime Period { get; set; }

        public int TaskID { get; set; }

        public int UserID { get; set; }
        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Remarks { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
    }

    public class MainTimeSheetView
    {
        public List<TimeSheetDetailsView> ListTimeSheetDetails { get; set; }
        public  List<GetPeriods> ListofPeriods { get; set; }
        public List<GetProjectNames> ListofProjectNames { get; set; }
        public List<string> ListoDayofWeek { get; set; }
        public int TimeSheetMasterID { get; set; }
    }

}
