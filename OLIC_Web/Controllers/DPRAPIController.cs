using BEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OLIC_Web.Controllers
{
    public class DPRAPIController : ApiController
    {
        OLICEntities db = new OLICEntities();

        [HttpGet]
        public IHttpActionResult DPRMaster()
        {
            var data = db.DBW_Mst_DPR.ToList();
            return Json(data);
        }


    }
}
