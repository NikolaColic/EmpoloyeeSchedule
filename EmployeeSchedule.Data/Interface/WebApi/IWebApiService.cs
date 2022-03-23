
using EmployeeSchedule.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Data.Interface.WebApi
{
    public interface IWebApiService
    {
        Task<bool> DeleteEmployee(int id);
        Task<IEnumerable<Company>> GetCompanies();
        Task<Schedule> GetScheduleById(int id);
        Task<bool> UpdateSchedule(Schedule schedule);
    }
}
