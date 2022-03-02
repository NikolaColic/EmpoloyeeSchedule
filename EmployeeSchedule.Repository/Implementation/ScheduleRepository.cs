﻿using EmployeeSchedule.Data;
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
    public class ScheduleRepository : IRepository<Schedule>
    {
        public readonly ApplicationDbContext _db;
        public ScheduleRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Delete(Schedule entity)
        {
            var oldEntity = await GetById(entity.Id);
            if (oldEntity == null)
            {
                throw new NullReferenceException("Entity not exist in database");
            }

            _db.Entry(oldEntity).State = EntityState.Deleted;
            return true;
        }

        public async Task<IEnumerable<Schedule>> GetAll()
        {
            var schedules = await _db.Schedule.ToListAsync();
            return schedules;
        }

        public async Task<Schedule> GetById(int id)
        {
            var schedule = await _db.Schedule.SingleOrDefaultAsync(e => e.Id == id);
            return schedule;
        }

        public async Task<bool> Insert(Schedule entity)
        {
            var employee = await _db.Employee.SingleOrDefaultAsync(e => e.Id == entity.Employee.Id);
            if (employee == null)
            {
                throw new NullReferenceException("Employee not exist in database");
            }

            entity.Employee = employee;

            await _db.AddAsync(entity);

            return true;
        }

        public async Task<bool> Update(Schedule entity)
        {
            var oldEntity = await GetById(entity.Id);
            if (oldEntity == null)
            {
                throw new NullReferenceException("Entity not exist in database");
            }

            var employee = await _db.Employee.SingleOrDefaultAsync(e => e.Id == entity.Employee.Id);
            if (employee == null)
            {
                throw new NullReferenceException("Employee not exist in database");
            }

            entity.Employee = employee;

            _db.Entry(oldEntity).State = EntityState.Detached;
            _db.Update(entity);

            return true;
        }
    }
}
