using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.Infrastructure.UnitOfWork.Interface;
using EmployeeSchedule.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.Service.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork<Schedule> _unitOfWork;
        public ScheduleService(IUnitOfWork<Schedule> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Delete(int id)
        {
            var entity = await GetById(id);
            var result = await _unitOfWork.Repository.Delete(entity);
            await _unitOfWork.Commit();
            return result;
        }

        public async Task<IEnumerable<Schedule>> GetAll()
        {
            var entities = await _unitOfWork.Repository.GetAll();
            return entities;
        }

        public async Task<Schedule> GetById(int id)
        {
            var entity = await _unitOfWork.Repository.GetById(id);
            return entity;
        }

        public async Task<IEnumerable<Schedule>> GetScheduleForEmployee(int id)
        {
            var repository = _unitOfWork.Repository as IScheduleRepository;
            var entity = await repository.GetScheduleForEmployee(id);
            await _unitOfWork.Commit();
            return entity;
        }

        public async Task<bool> Insert(Schedule entity)
        {
            if (await ScheduleExist(entity))
            {
                throw new Exception($"Schedule for employee {entity.Employee.Email} on date {entity.Date} exist");
            }

            var result = await _unitOfWork.Repository.Insert(entity);
            await _unitOfWork.Commit();
            return result;
        }

        public async Task<bool> Update(Schedule entity)
        {
            if (await ScheduleExist(entity))
            {
                throw new Exception($"Schedule for employee {entity.Employee.Email} on date {entity.Date} exist");
            }

            var result = await _unitOfWork.Repository.Update(entity);
            await _unitOfWork.Commit();
            return result;
        }

        private async Task<bool> ScheduleExist(Schedule schedule)
        {
            var schedules = await GetAll();
            return schedules.Any(e => e.Employee.Id == schedule.Employee.Id && e.Date.Date == schedule.Date.Date && e.Id != schedule.Id);
        }
    }
}
