using BEL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OLIC_Web.Controllers
{
    public class FarmerController : Controller
    {
        // GET: Farmer
        private readonly BLL.AbstractLayer applicationRepository;

        OLICEntities db = new OLICEntities();

        public FarmerController(BLL.AbstractLayer apprepo)
        {
            applicationRepository = apprepo;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Application()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeepBorewell()
        {
            ViewBag.District = applicationRepository.District();
            ViewBag.Block = applicationRepository.Block();
            ViewBag.GramPanchayat = applicationRepository.GramPanchayat();
            return View();
        }

        [HttpPost]
        public ActionResult DeepBorewell(FarmerRegistration mdl, HttpPostedFileBase Adhar, HttpPostedFileBase Bpl, HttpPostedFileBase Photo, HttpPostedFileBase Affidavit, string[] Plot, string[] Khata, string[] Land, HttpPostedFileBase[] Patta)
        {
            try
            {
                string fileloc = "/FileUploadFolder/";

                if (Adhar != null)
                {
                    var Adharr = UploadFileHelper.Benificiary_SaveFileIntoLocal(Adhar, FileTypes.AadhaarFile);
                    mdl.DB_Registration.AadhaarFile = fileloc + Adharr.SystemFileName;
                }
                if (Bpl != null)
                {
                    var Bplcard = UploadFileHelper.Benificiary_SaveFileIntoLocal(Bpl, FileTypes.BPLCardFile);
                    mdl.DB_Registration.BPLCardFile = fileloc + Bplcard.SystemFileName;
                }
                if (Photo != null)
                {
                    var FarmerPhoto = UploadFileHelper.Benificiary_SaveFileIntoLocal(Photo, FileTypes.FarmerPhoto);
                    mdl.DB_Registration.FarmerPhoto = fileloc + FarmerPhoto.SystemFileName;
                }
                if (Affidavit != null)
                {
                    var AffidavitDtl = UploadFileHelper.Benificiary_SaveFileIntoLocal(Affidavit, FileTypes.AffidavitFile);
                    mdl.DB_Registration.AffidavitFile = fileloc + AffidavitDtl.SystemFileName;
                }

                List<DeepBorewell_PattaDetailsdetails> pdetail = new List<DeepBorewell_PattaDetailsdetails>();

                if (Patta[0].ContentLength > 0)
                {
                    for (int i = 0; i < Patta.Length; i++)
                    {
                        DeepBorewell_PattaDetailsdetails q = new DeepBorewell_PattaDetailsdetails();
                        var bhg = UploadFileHelper.Benificiary_SaveFileIntoLocal(Patta[i], FileTypes.LandPattaFile);

                        q.Khata = Khata[i];
                        q.Land = Land[i];
                        q.Plot = Plot[i];
                        q.Patta = fileloc + bhg.SystemFileName;
                        pdetail.Add(q);
                        q = new DeepBorewell_PattaDetailsdetails();
                    }
                }
                mdl.listdeeppatta = pdetail.ToList();
                int count = applicationRepository.SaveDeepBorewellApplication(mdl);

                if (count == 1)
                {
                    TempData["Message"] = "Registration Completed Successfully";
                }
                else
                {
                    TempData["Message"] = "Registration Details Updated";
                }
                return RedirectToAction("DeepBorewell", "Farmer");

            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something Went Wrong";
                return RedirectToAction("DeepBorewell", "Farmer");
            }

        }

        [HttpPost]
        public ActionResult BlockType(int cccc)
        {
            var list = db.OLIC_Block_Ulb.Where(x => x.District_Id == cccc).ToList();
            List<SelectListItem> BlockName = new List<SelectListItem>();
            if (cccc != 0)
            {
                list.ForEach(x =>
                {
                    BlockName.Add(new SelectListItem { Text = x.Block_Ulb_Name, Value = x.Block_Ulb_Id.ToString() });
                });
            }
            return Json(BlockName, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GramPanchayatType(int gggg)
        {
            var list = db.OLIC_Gram_Panchayat.Where(x => x.Block_Ulb_Id == gggg).ToList();
            List<SelectListItem> GramPanchayatName = new List<SelectListItem>();
            if (gggg != 0)
            {
                list.ForEach(x =>
                {
                    GramPanchayatName.Add(new SelectListItem { Text = x.GRAM_PANCHAYAT_Name, Value = x.GarmPanchayat_Id.ToString() });
                });
            }
            return Json(GramPanchayatName, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SolarDeepBorewell()
        {
            return View();
        }

        public ActionResult CLIP()
        {
            return View();
        }

        public ActionResult ApplicationStatus()
        {
            return View();
        }
    }
}