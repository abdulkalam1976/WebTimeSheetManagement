using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;


namespace WebTimeSheetManagement.Interface
{
    public interface IEnumeration
    {
        List<Enumeration> GetListofEnumeration();
        List<Enumeration> GetListofEnumerationBasedOnType(int type);

    }
}
