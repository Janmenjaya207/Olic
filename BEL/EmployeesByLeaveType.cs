using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    public class EmployeesByLeaveType
    {
        public int EmployeeID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Remarks { get; set; }
        public int Count { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public string Remarks3 { get; set; }
        public string Remarks4 { get; set; }
        public string Remarks5 { get; set; }

        public int Approval1 { get; set; }
        public int Approval2 { get; set; }
        public int Approvale3 { get; set; }
        public int Approval4 { get; set; }
        public int Approval5 { get; set; }
        public string Approvedreject1 { get; set; }
        public string Approvedreject2 { get; set; }

        public string Approvedreject3 { get; set; }
        public string Approvedreject4 { get; set; }
        public string Approvedreject5 { get; set; }
        public int Addby { get; set; }
        public int Addon { get; set; }
        public int Modby { get; set; }
        public int Modon { get; set; }
        public string Employee_Name { get; set; }
        public string LeaveType { get; set; }

    }
}