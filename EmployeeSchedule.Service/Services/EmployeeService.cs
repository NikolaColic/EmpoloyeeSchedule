using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.Infrastructure.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSchedule.Service.Services
{
    public class EmployeeService : IGenericService<Employee>
    {
        private readonly IUnitOfWork<Employee> _unitOfWork;
        public EmployeeService(IUnitOfWork<Employee> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await GetById(id);
                if (entity == null)
                {
                    return false;
                }

                var result = await _unitOfWork.Repository.Delete(entity);
                await _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            try
            {
                var entities = await _unitOfWork.Repository.GetAll();
                return entities;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<Employee> GetById(int id)
        {
            try
            {
                var entity = await _unitOfWork.Repository.GetById(id);
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<bool> Insert(Employee entity)
        {
            try
            {
                var result = await _unitOfWork.Repository.Insert(entity);
                await _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> Update(Employee entity)
        {
            try
            {
                var result = await _unitOfWork.Repository.Update(entity);
                await _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
