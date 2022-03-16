using EmployeeSchedule.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.MVC.Models.Create
{
    public class EmployeeCreate : Employee
    {
        [TempData]
        public bool Result { get; set; }
        public List<SelectListItem> CompaniesSelectList { get; set; }
        public EmployeeCreate()
        {
            Company = new Company();
        }
        public void CompanySelectList(IEnumerable<Company> companies)
        {
            CompaniesSelectList = companies.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();
        }

    }
}
