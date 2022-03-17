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
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IGenericService<Employee> _employeeService;
        private readonly IMapper _mapper;

        public ScheduleController(IScheduleService scheduleService, IGenericService<Employee> employeeService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _employeeService = employeeService;
            _mapper = mapper;
        }


        // GET: ScheduleController
        public async Task<ActionResult> Index()
        {
            var schedules = await _scheduleService.GetAll();
            return View(_mapper.Map<List<ScheduleViewModel>>(schedules));
        }

        // GET: ScheduleController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var schedule = await _scheduleService.GetById(id);
            return View(_mapper.Map<ScheduleCreate>(schedule));
        }

        // GET: ScheduleController/Create
        public async Task<ActionResult> Create()
        {
            var employees = await _employeeService.GetAll();
            var employeeCreate = new ScheduleCreate();
            employeeCreate.EmployeeSelectList(employees);
            return View(employeeCreate);
        }

        // POST: ScheduleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ScheduleCreate scheduleCreate)
        {
            var schedule = _mapper.Map<Schedule>(scheduleCreate);
            scheduleCreate.Result = await _scheduleService.Insert(schedule);
            scheduleCreate.EmployeeSelectList(await _employeeService.GetAll());
            return View(scheduleCreate);
        }

        // GET: ScheduleController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var schedule = await _scheduleService.GetById(id);
            var scheduleCreate = _mapper.Map<ScheduleCreate>(schedule);
            var employees = await _employeeService.GetAll();
            scheduleCreate.EmployeeSelectList(employees);
            return View(scheduleCreate);
        }

        // POST: ScheduleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ScheduleCreate scheduleCreate)
        {
            scheduleCreate.Id = id;
            var employee = _mapper.Map<Schedule>(scheduleCreate);
            scheduleCreate.Result = await _scheduleService.Update(employee);
            scheduleCreate.EmployeeSelectList(await _employeeService.GetAll());
            return View(scheduleCreate);
        }

        // GET: ScheduleController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _scheduleService.Delete(id);

            if (!result)
            {
                TempData["DeleteError"] = true;
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
