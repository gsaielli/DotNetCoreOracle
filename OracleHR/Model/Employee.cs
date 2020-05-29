using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OracleHR.Model
{
    public class Employee
    {
        public int employee_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public DateTime hire_date { get; set; }
        public string job_id { get; set; }
        public float? salary { get; set; }
        public float? commission_pct { get; set; }
        public float? manager_id { get; set; }
        public float? department_id { get; set; }
    }
}
