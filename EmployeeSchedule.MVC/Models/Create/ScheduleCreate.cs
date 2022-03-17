
using EmployeeSchedule.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.MVC.Models.Create
{
    public class ScheduleCreate : Schedule
    {
        [TempData]
        public bool Result { get; set; }
        public List<SelectListItem> EmployeesSelectList { get; set; }
        public ScheduleCreate()
        {
            Employee = new Employee();
        }
        public void EmployeeSelectList(IEnumerable<Employee> employees)
        {
            EmployeesSelectList = employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name + e.Surname
            }).ToList();
        }
    }
}
