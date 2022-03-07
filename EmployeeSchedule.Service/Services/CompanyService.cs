﻿using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.Infrastructure.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSchedule.Service.Services
{
    public class CompanyService : IGenericService<Company>
    {
        private readonly IUnitOfWork<Company> _unitOfWork;
        public CompanyService(IUnitOfWork<Company> unitOfWork)
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
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public async Task<IEnumerable<Company>> GetAll()
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

        public async Task<Company> GetById(int id)
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

        public async Task<bool> Insert(Company entity)
        {
            try
            {
                var result = await _unitOfWork.Repository.Insert(entity);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> Update(Company entity)
        {
            try
            {
                var result = await _unitOfWork.Repository.Update(entity);
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
