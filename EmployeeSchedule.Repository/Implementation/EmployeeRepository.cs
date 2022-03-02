using EmployeeSchedule.Data;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Repository.Implementation
{
    public class EmployeeRepository : IRepository<Employee>
    {
        public readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Delete(Employee entity)
        {
            var oldEntity = await GetById(entity.Id);
            if (oldEntity == null)
            {
                throw new NullReferenceException("Entity not exist in database");
            }

            _db.Entry(oldEntity).State = EntityState.Deleted;
            return true;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var employees = await _db.Employee
                .Include(e => e.Company)
                .ToListAsync();
            return employees;
        }

        public async Task<Employee> GetById(int id)
        {
            var employee = await _db.Employee.SingleOrDefaultAsync(e => e.Id == id);
            return employee;
        }

        public async Task<bool> Insert(Employee entity)
        {
            var company = await _db.Company.SingleOrDefaultAsync(e => e.Id == entity.Company.Id);
            if(company == null)
            {
                throw new NullReferenceException("Company not exist in database");
            }

            entity.Company = company;

            await _db.AddAsync(entity);
            
            return true;
        }

        public async Task<bool> Update(Employee entity)
        {
            var oldEntity = await GetById(entity.Id);
            if (oldEntity == null)
            {
                throw new NullReferenceException("Entity not exist in database");
            }

            var company = await _db.Company.SingleOrDefaultAsync(e => e.Id == entity.Company.Id);
            if (company == null)
            {
                throw new NullReferenceException("Company not exist in database");
            }

            entity.Company = company;

            _db.Entry(oldEntity).State = EntityState.Detached;
            _db.Update(entity);

            return true;
        }
    }
}
