using AutoMapper;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.MVC.Models.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.MVC.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IGenericService<Company> _service;
        private readonly IMapper _mapper;
        public CompanyController(IGenericService<Company> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        // GET: CompanyController
        public ActionResult Index()
        {
            return View(new CompanyCreate());
        }

        // GET: CompanyController/Details/5
        public ActionResult Details(CompanyCreate companyView)
        {
            return View();
        }

        // GET: CompanyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CompanyCreate companyCreate)
        {
            var company = _mapper.Map<Company>(companyCreate);
            companyCreate.Result = await _service.Insert(company);
            return View(companyCreate);
        }

        // GET: CompanyController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var company = await _service.GetById(id);
            return View(_mapper.Map<CompanyCreate>(company));
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CompanyCreate companyCreate)
        {
            var company = _mapper.Map<Company>(companyCreate);
            companyCreate.Result = await _service.Update(company);
            return View(companyCreate);
        }

        // GET: CompanyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompanyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
