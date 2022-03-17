using AutoMapper;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.MVC.Models.Create;
using EmployeeSchedule.MVC.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IGenericService<Company> _companyService;
        private readonly IGenericService<Employee> _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IGenericService<Employee> employeeService, IGenericService<Company> companyService, IMapper mapper)
        {
            _employeeService = employeeService;
            _companyService = companyService;
            _mapper = mapper;
        }

        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            var employees = await _employeeService.GetAll();
            return View(_mapper.Map<List<EmployeeViewModel>>(employees));
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var employee = await _employeeService.GetById(id);
            var employeeCreate = _mapper.Map<EmployeeCreate>(employee);
            return View(employeeCreate);
        }

        // GET: EmployeeController/Create
        public async Task<ActionResult> Create()
        {
            var companies = await _companyService.GetAll();
            var employeeCreate = new EmployeeCreate();
            employeeCreate.CompanySelectList(companies);
            return View(employeeCreate);
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeCreate employeeCreate)
        {
            var employee = _mapper.Map<Employee>(employeeCreate);
            employeeCreate.Result = await _employeeService.Insert(employee);
            employeeCreate.CompanySelectList(await _companyService.GetAll());
            return View(employeeCreate);
        }

        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetById(id);
            var employeeCreate = _mapper.Map<EmployeeCreate>(employee);
            var companies = await _companyService.GetAll();
            employeeCreate.CompanySelectList(companies);
            return View(employeeCreate);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EmployeeCreate employeeCreate)
        {
            employeeCreate.Id = id;
            var employee = _mapper.Map<Employee>(employeeCreate);
            employeeCreate.Result = await _employeeService.Update(employee);
            employeeCreate.CompanySelectList(await _companyService.GetAll());
            return View(employeeCreate);
        }

        // GET: EmployeeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _employeeService.Delete(id);

            if(!result)
            {
                TempData["DeleteError"] = true;
            }

            return RedirectToAction(nameof(Index));
        }
        
    }
}
