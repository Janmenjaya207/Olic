using BEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OLIC_Web.Controllers
{
    public class BillingSectionController : Controller
    {
        private readonly BLL.AbstractLayer applicationRepository;

        OLICEntities db = new OLICEntities();

        public BillingSectionController(BLL.AbstractLayer appRepository)
        {
            applicationRepository = appRepository;
        }
        // GET: BillingSection
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult DBW_BillingApproval()
        {
            RajeshDemoModel dprEstimations = new RajeshDemoModel();
            
            ViewBag.DprDtl = applicationRepository.dBW_Mst_DPRs();
            ViewBag.total = Convert.ToDouble(applicationRepository.dBW_Mst_DPRs().Select(x => x.Rate_per_Unit).Sum());
            double? total = Convert.ToDouble(ViewBag.total);
            ViewBag.subtotalprcnt = (ViewBag.total * 8.5) / 100;
            ViewBag.subtotal = ((ViewBag.total * 8.5) / 100) + ViewBag.total;
            double? subtotal = Convert.ToDouble(ViewBag.subtotal);
            ViewBag.grandtotalprcnt = (ViewBag.subtotal * 12) / 100;
            ViewBag.grandtotal = ((ViewBag.subtotal * 12) / 100) + ViewBag.subtotal;


            ViewBag.a1 = Math.Round(Convert.ToDecimal(ViewBag.total), 2);
            ViewBag.a2 = Math.Round(Convert.ToDecimal(ViewBag.subtotalprcnt), 2);
            ViewBag.a3 = Math.Round(Convert.ToDecimal(ViewBag.subtotal), 2);
            ViewBag.a4 = Math.Round(Convert.ToDecimal(ViewBag.grandtotalprcnt), 2);
            ViewBag.a5 = Math.Round(Convert.ToDecimal(ViewBag.grandtotal), 2);
            ViewBag.a6 = Math.Round(Convert.ToDecimal(ViewBag.grandtotal));

            ViewBag.Registeration = db.DeepBorewell_Registration.ToList();


            //dprEstimations.vw_DprEstimations = applicationRepository.vw_DprEstimations();
            return View(dprEstimations);
        }

        public ActionResult DBW_CheckBillStatus()
        {
            ViewBag.CheckDprEstimation = applicationRepository.vw_DprEstimations();

            return View();
        }

        [HttpPost]
        public ActionResult ViewDetails(string id)
        {
           
            var data = applicationRepository.vw_DprEstimations().Where(x => x.Application_Number == id).ToList();
     
            return PartialView("_ViewFarmerBilling",data);
        }
        public ActionResult CLIP_BillingApproval()
        {
            return View();
        }
        public ActionResult CLIP_CheckBillStatus()
        {

           
            return View();
        }

        [HttpPost]
        public ActionResult DBW_BillingApproval(int[] Dpr_Id,string[] quantity,string[] Length, string[] Breadth, string[] Height, string[] Content_Area, string[] Remark, string ApplicationNonn)
        {
            ViewBag.Registeration = db.DeepBorewell_Registration.ToList();
            RajeshDemoModel applicationModel = new RajeshDemoModel();

            List<DBW_BILLING> pdetail = new List<DBW_BILLING>();

            if (quantity != null)
            {
                for (int i = 0; i < quantity.Length; i++)
                {
                    if (quantity[i]!="")
                    {
                        DBW_BILLING p = new DBW_BILLING();
                        p.Length = Length[i];
                        p.Breadth = Breadth[i];
                        p.Height = Height[i];
                        p.Content_Area = Content_Area[i];
                        p.Quantity = quantity[i];
                        p.Remark = Remark[i];
                        p.Dpr_Id = Dpr_Id[i];
                        pdetail.Add(p);
                        p = new DBW_BILLING();
                    }
                }
            }
            applicationModel.dBW_BILLINGs = pdetail.ToList();
            applicationModel.ApplicationNo = ApplicationNonn;

         int count=   applicationRepository.SaveDBW(applicationModel);

            return RedirectToAction("DBW_BillingApproval");
        }
    }
}