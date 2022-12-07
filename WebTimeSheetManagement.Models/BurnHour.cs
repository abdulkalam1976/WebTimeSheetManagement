using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    public class BurnHour
    {
        public string UserName { get; set; }
        public decimal EstimatedHour { get; set; }
        public decimal TotalHours { get; set; }
    }
}
