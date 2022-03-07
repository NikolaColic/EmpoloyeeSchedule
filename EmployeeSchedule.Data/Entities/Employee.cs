using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Data.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string Number { get; set; }
        public string Email { get; set;  }
        public string Password { get; set; }
        public string Possition { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public bool Administrator { get; set; }
    }
}
