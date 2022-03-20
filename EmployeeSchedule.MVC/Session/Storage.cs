using EmployeeSchedule.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.MVC.Session
{
    public class Storage
    {
        private static Storage instance; 
        public Employee LoginEmployee { get; set; }
        public LoginCurrentRole IsAdmin { get; set; } 
        private Storage()
        {
            IsAdmin = LoginCurrentRole.No;
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

    public enum LoginCurrentRole
    {
        Admin,Employee,No
    }
}
