﻿using AutoMapper;
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
        public async Task<ActionResult> Index()
        {
            var companies = await _service.GetAll();
            return View(_mapper.Map<List<CompanyCreate>>(companies));
        }

        // GET: CompanyController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var company = await _service.GetById(id);
                return View(_mapper.Map<CompanyCreate>(company));
            }
            catch (Exception ex)
            {
                return View(new CompanyCreate(ex.Message));
            }
        }

        // GET: CompanyController/Create
        public ActionResult Create()
        {
            return View(new CompanyCreate());
        }

        // POST: CompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CompanyCreate companyCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(companyCreate);
                }

                var company = _mapper.Map<Company>(companyCreate);
                await _service.Insert(company);
                return View(companyCreate);
            }
            catch (Exception ex)
            {
                return View(new CompanyCreate(ex.Message));
            }
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
            try
            {
                companyCreate.Id = id;

                if (!ModelState.IsValid)
                {
                    return View(companyCreate);
                }

                var company = _mapper.Map<Company>(companyCreate);
                await _service.Update(company);
                return View(companyCreate);
            }
            catch (Exception  ex)
            {
                return View(new CompanyCreate(ex.Message));
            }
        }

    }
}
