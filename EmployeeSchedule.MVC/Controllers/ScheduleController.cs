using AutoMapper;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.MVC.Models.Create;
using EmployeeSchedule.MVC.Models.ViewModel;
using EmployeeSchedule.MVC.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.MVC.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public ScheduleController(IScheduleService scheduleService, IEmployeeService employeeService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _employeeService = employeeService;
            _mapper = mapper;
        }


        // GET: ScheduleController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Schedule> schedules;

            if(Storage.Instance.IsAdmin == LoginCurrentRole.Admin)
            {
                schedules = await _scheduleService.GetAll();
            }
            else
            {
                schedules = await _scheduleService.GetScheduleForEmployee(Storage.Instance.LoginEmployee.Id);
            }
            var employees = await _employeeService.GetAll();

            ViewBag.EmployeeSelectList = employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name + e.Surname
            }).ToList();

            if(Storage.Instance.IsAdmin == LoginCurrentRole.Admin)
            {
                ViewBag.NumberOfRequests = schedules.Count(e => string.IsNullOrEmpty(e.ShiftWork));
            }
            else
            {
                ViewBag.CreateScheduleEnabled = !schedules.Any(e => e.Date.Date == DateTime.Now.Date && e.Employee.Id == Storage.Instance.LoginEmployee.Id);
            }

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
            try
            {
                if (Storage.Instance.IsAdmin == LoginCurrentRole.Employee)
                {
                    var schedule = new Schedule()
                    {
                        Employee = Storage.Instance.LoginEmployee,
                        CheckInTime = DateTime.Now,
                        Date = DateTime.Now,
                    };

                    _ = await _scheduleService.Insert(schedule);
                    return RedirectToAction(nameof(Index));

                }

                var employees = await _employeeService.GetAll();
                var scheduleCreate = new ScheduleCreate();
                scheduleCreate.EmployeeSelectList(employees);
                return View(scheduleCreate);
            }
            catch (Exception ex)
            {
                return View(new ScheduleCreate(ex.Message));
            }
        }

        // POST: ScheduleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ScheduleCreate scheduleCreate)
        {
            try
            {
                scheduleCreate.EmployeeSelectList(await _employeeService.GetAll());

                if (!ModelState.IsValid)
                {
                    return View(scheduleCreate);
                }

                var schedule = _mapper.Map<Schedule>(scheduleCreate);
                await _scheduleService.Insert(schedule);
                return View(scheduleCreate);
            }
            catch (Exception ex)
            {
                return View(new ScheduleCreate(ex.Message));
            }
        }

        // GET: ScheduleController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var schedule = await _scheduleService.GetById(id);

            if (Storage.Instance.IsAdmin == LoginCurrentRole.Employee)
            {
                schedule.CheckInTime = DateTime.Now;
                _ = await _scheduleService.Update(schedule);
                return RedirectToAction(nameof(Index));
            }

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
            try
            {
                scheduleCreate.Id = id;

                scheduleCreate.EmployeeSelectList(await _employeeService.GetAll());

                if (!ModelState.IsValid)
                {
                    return View(scheduleCreate);
                }

                var employee = _mapper.Map<Schedule>(scheduleCreate);
                await _scheduleService.Update(employee);
                return View(scheduleCreate);
            }
            catch (Exception ex) 
            {
                return View(new ScheduleCreate(ex.Message));
            }
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

        public async Task<ActionResult> Search(string text, string employeeId, DateTime date)
        {
            text ??= string.Empty;
            var schedules = await _scheduleService.GetAll();

            schedules = schedules.Where(e => (e.Notification.ToLower().Contains(text.ToLower()) || e.ShiftWork.ToLower().Contains(text.ToLower())) 
            && (string.IsNullOrEmpty(employeeId) || e.Employee.Id.ToString() == employeeId) && (date == DateTime.MinValue || e.Date.Date == date.Date)).ToList();

            return PartialView(_mapper.Map<List<ScheduleViewModel>>(schedules));
        }

    }
}
