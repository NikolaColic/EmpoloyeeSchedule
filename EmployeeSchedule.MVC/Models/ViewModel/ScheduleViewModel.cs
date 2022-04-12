using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Entities.ApiEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.MVC.Models.ViewModel
{
    public class ScheduleViewModel : Schedule
    {
        public CheckInStatus CheckInStatus { get; set; }
        public ScheduleViewModel()
        {

            if ((CheckInTime == DateTime.MinValue))
            {
                if(Date.Date < DateTime.Now.Date)
                {
                    CheckInStatus = CheckInStatus.NotRequired;
                    return;
                }

                CheckInStatus = Date > DateTime.Now ? CheckInStatus.Late : CheckInStatus.CheckIn;
                return;
            }
            else
            {
                CheckInStatus = ShiftWork == "Prva" && CheckInTime.Hour < 8 ? CheckInStatus.OnTime 
                : ShiftWork == "Druga" && CheckInTime.Hour < 15 ? CheckInStatus.OnTime : CheckInStatus.Late;
            }

        }
    }

    public enum CheckInStatus
    {
        Late, OnTime, CheckIn, NotRequired
    }
}
