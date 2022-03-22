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
        public CheckInStatus CheckInStatus { get; set; }
        public ScheduleViewModel()
        {
            if(Date.Date < DateTime.Now.Date)
            {
                CheckInStatus = CheckInStatus.NotRequired;
                return;
            }

            if ((CheckInTime == DateTime.MinValue) && (Date > DateTime.Now))
            {
                CheckInStatus = Date > DateTime.Now ? CheckInStatus.CheckIn : CheckInStatus.Late;
                return;
            }

            CheckInStatus = ShiftWork == "Prva" && CheckInTime.Hour < 8 ? CheckInStatus.OnTime 
            : ShiftWork == "Druga" && CheckInTime.Hour < 15 ? CheckInStatus.OnTime : CheckInStatus.Late;
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

    public enum CheckInStatus
    {
        Late, OnTime, CheckIn, NotRequired
    }
}
