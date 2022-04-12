﻿using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.Infrastructure.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork<Employee> _unitOfWork;
        public EmployeeService(IUnitOfWork<Employee> unitOfWork)
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

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var entities = await _unitOfWork.Repository.GetAll();
            return entities;
        }

        public async Task<Employee> GetById(int id)
        {
            var entity = await _unitOfWork.Repository.GetById(id);
            return entity;
        }

        public async Task<bool> Insert(Employee entity)
        {
            if(await EmployeeExist(entity))
            {
                throw new Exception("Employee exist");
            }

            var result = await _unitOfWork.Repository.Insert(entity);
            await _unitOfWork.Commit();
            return result;
        }

        public async Task<Employee> Login(string email, string password)
        {
            var employees = await GetAll();
            var loginEmployee = employees.SingleOrDefault(e => e.Email == email && e.Password == password);
            return loginEmployee;
        }

        public async Task<IEnumerable<Employee>> Search(string criteria)
        {
            var employees = await GetAll();

            employees = employees.Where(e => e.Name.ToLower().Contains(criteria.ToLower()) || e.Surname.ToLower().Contains(criteria.ToLower())
            || e.Adress.ToLower().Contains(criteria.ToLower()) || e.Number.ToLower().Contains(criteria.ToLower()) || e.Email.ToLower().Contains(criteria.ToLower())
            || e.Possition.ToLower().Contains(criteria.ToLower())).ToList();

            return employees;
        }

        public async Task<IEnumerable<Employee>> Sort(string criteria, string criteria2 = null, DateTime date = default)
        {
            var employees = await GetAll();

            switch (criteria)
            {
                case "Name":
                    employees = employees.OrderBy(e => e.Name);
                    break;
                case "Surname":
                    employees = employees.OrderBy(e => e.Surname);
                    break;
                case "Email":
                    employees = employees.OrderBy(e => e.Email);
                    break;
                case "Adress":
                    employees = employees.OrderBy(e => e.Adress);
                    break;
                case "Possition":
                    employees = employees.OrderBy(e => e.Possition);
                    break;
            }

            return employees;
        }

        public async Task<bool> Update(Employee entity)
        {
            if (await EmployeeExist(entity))
            {
                throw new Exception("Employee exist");
            }

            var result = await _unitOfWork.Repository.Update(entity);
            await _unitOfWork.Commit();
            return result;
        }

        private async Task<bool> EmployeeExist(Employee employee)
        {
            var employees = await GetAll();
            return employees.Any(e => (e.Email == employee.Email || e.Number == employee.Number) && e.Id != employee.Id);
        }
    }
}
