using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace WebTimeSheetManagement.Concrete
{
    public class EnumerationConcrete : IEnumeration
    {
        public List<Enumeration> GetListofEnumeration()
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var listofEnums = (from enums in _context.Enumerations
                                          select enums).ToList();
                    return listofEnums;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Enumeration> GetListofEnumerationBasedOnType(int typeid)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var listofEnums = (from enums in _context.Enumerations where enums.Enumtype == typeid
                                       select enums).ToList();
                    return listofEnums;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
