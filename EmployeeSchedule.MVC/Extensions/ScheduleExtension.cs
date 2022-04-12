using EmployeeSchedule.MVC.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.MVC.Extensions
{
    public static class ScheduleExtension
    {
        public static void SetCheckInStatus(this ScheduleViewModel schedule)
        {
            if ((schedule.CheckInTime == DateTime.MinValue))
            {
                if (schedule.Date.Date < DateTime.Now.Date)
                {
                    schedule.CheckInStatus = CheckInStatus.NotRequired;
                    return;
                }

                schedule.CheckInStatus = schedule.Date > DateTime.Now ? CheckInStatus.Late : CheckInStatus.CheckIn;
                return;
            }
            else
            {
                schedule.CheckInStatus = schedule.ShiftWork == "Prva" && schedule.CheckInTime.Hour < 8 ? CheckInStatus.OnTime
                : schedule.ShiftWork == "Druga" && schedule.CheckInTime.Hour < 15 ? CheckInStatus.OnTime : CheckInStatus.Late;
            }
        }
    }
}
