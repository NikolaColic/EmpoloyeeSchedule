using EmployeeSchedule.Data.Entities.ApiEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Service.Storage
{
    public class Storage
    {
        private static Storage instance; 
        public List<City> Cities { get; set; }
        public List<Holiday> Holidays { get; set; }
        private Storage()
        {

        }
        public static Storage Instance
        {
            get
            {
                instance ??= new Storage();
                return instance;
            }
        }
    }
}
