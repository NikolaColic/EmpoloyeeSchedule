using EmployeeSchedule.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Data.Interface
{
    public interface IEmployeeService : IGenericService<Employee>
    {
        Task<Employee> Login(string email, string password);
    }
}
