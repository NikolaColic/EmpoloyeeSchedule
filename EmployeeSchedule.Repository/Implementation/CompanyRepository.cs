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
    public class CompanyRepository : IRepository<Company>
    {
        public readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Delete(Company entity)
        {
            var oldEntity = await GetById(entity.Id);
            if (oldEntity == null)
            {
                throw new NullReferenceException("Entity not exist in database");
            }

            _db.Entry(oldEntity).State = EntityState.Deleted;
            return true;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            var companies = await _db.Company.ToListAsync();
            return companies;
        }

        public async Task<Company> GetById(int id)
        {
            var company = await _db.Company.SingleOrDefaultAsync(e => e.Id == id);
            return company;
        }

        public async Task<bool> Insert(Company entity)
        {
            await _db.AddAsync(entity);
            return true;
        }

        public async Task<bool> Update(Company entity)
        {
            var oldEntity = await GetById(entity.Id);
            if (oldEntity == null)
            {
                throw new NullReferenceException("Entity not exist in database");
            }

            _db.Entry(oldEntity).State = EntityState.Detached;
            _db.Update(entity);

            return true;
        }
    }
}
