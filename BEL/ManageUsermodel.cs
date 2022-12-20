using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BEL
{
    public class ManageUsermodel
    {
        public ManageUser mangeuser { get; set; }
        public List<SelectListItem> Getsection { get; set; }
        public List<SelectListItem> GetDivision { get; set; }
        public List<SelectListItem> GetCircle { get; set; }
        public List<SelectListItem> GetSubdivision { get; set; }
        public List<SelectListItem> GetDistrcit { get; set; }
        public List<SelectListItem> GetBlock { get; set; }
        public List<SelectListItem> GetPanchYat { get; set; }
        public List<SelectListItem> GetUserRole { get; set; }

    }
}
