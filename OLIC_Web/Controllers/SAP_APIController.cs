using BEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OLIC_Web.Controllers
{

    public class SAP_APIController : ApiController
    {

        OLICEntities db = new OLICEntities();


        public IHttpActionResult GetApi()
        {
            var data = db.DB_Registration_Detail.ToList();
            return Json(data);
        }

        [HttpGet]
        public IHttpActionResult GetData()
        {
            List<Demo> demos = new List<Demo>();

            var data = db.Demo_Table_Rajesh.ToList();
            foreach(var iteam in data)
            {
                Demo d = new Demo();
                d.service_code = iteam.Service_Code;
                d.Application_no = iteam.Application_Number;
                d.quantity = iteam.Quantity;
                demos.Add(d);

            }
            return Json(demos);
        }


        public class Demo
        {
            public string service_code { get; set; }
            public string Application_no { get; set; }
            public string quantity { get; set; }
        }
    }
}
