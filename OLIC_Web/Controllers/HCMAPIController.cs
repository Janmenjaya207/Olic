using BEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OLIC_Web.Controllers
{
    public class HCMAPIController : ApiController
    {
        //public readonly BLL.AbstractLayer applicationRepository;

        OLICEntities db = new OLICEntities();


        //public HCMAPIController(BLL.AbstractLayer apprepo)
        //{
        //    applicationRepository = apprepo;
        //}
        [HttpGet]
        public IHttpActionResult Getemployeedetails()
        {
            var data = db.vwEmployeesByLeaveTypes.Where(x => x.Approverejct == "Approved").ToList();
            return Json(data);
        }
    }
}
