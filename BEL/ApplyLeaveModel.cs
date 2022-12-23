using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BEL
{
    public class ApplyLeaveModel
    {
        public Hcm_ApplyLeave applyLeavelr { get; set; }


        public List<SelectListItem> getleavetype { get; set; }
        public class Getemployeedetils
        {
            public string emplyeecode { get; set; }
            public string Email_Id { get; set; }
            public string Employee_Name { get; set; }
            public string Mobile_No { get; set; }
        }
        public class DeleteLeaveModel
        {
            public Hcm_ApplyLeave applyLeavelr { get; set; }
            public List<SelectListItem> getleavetype { get; set; }
        }

    }
}