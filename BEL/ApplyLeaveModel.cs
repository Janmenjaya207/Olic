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
    }
}