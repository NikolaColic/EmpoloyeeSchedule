﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Data.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public string ShiftWork { get; set; }
        public DateTime CheckInTime { get; set; }
        public bool Late { get; set; }
        public string Notification { get; set; }
    }
}