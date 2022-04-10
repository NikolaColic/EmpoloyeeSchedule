using EmployeeSchedule.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Data.Interface.Pdf
{
    public interface IPdfService
    {
        Task<string> GeneratePdfScheduleForEmployee(List<Schedule> schedules);
    }
}
