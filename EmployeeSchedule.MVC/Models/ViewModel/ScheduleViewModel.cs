using EmployeeSchedule.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.MVC.Models.ViewModel
{
    public class ScheduleViewModel : Schedule
    {
        public List<SelectListItem> EmployeesSelectList { get; set; }
       
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
