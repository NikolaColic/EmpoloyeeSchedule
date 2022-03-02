using EmployeeSchedule.Data;
using EmployeeSchedule.Infrastructure.UnitOfWork.Interface;
using EmployeeSchedule.Repository;
using EmployeeSchedule.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Infrastructure.UnitOfWork.Implementation
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T: class
    {
        public readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IRepository<T> Repository { get; set; }

        public Task<T> Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
