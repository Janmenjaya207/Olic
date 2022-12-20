using BEL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace OLIC_Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly BLL.AbstractLayer applicationRepository;

        OLICEntities db = new OLICEntities();

        public AdminController(BLL.AbstractLayer appRepository)
        {
            applicationRepository = appRepository;
        }
        // GET: Admin
        public ActionResult Index()
        {
            //var data = applicationRepository.oLIC_Tests();
            return View();
        }


        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DBW_DPR_Master(int? id = 0)
        {
            if (id != 0)
            {
                MasterModel masterModel = new MasterModel();
                masterModel.DBW_Mst_DPR = db.DBW_Mst_DPR.Where(x => x.Dpr_Id == id).FirstOrDefault();
                ViewBag.DprDtl = applicationRepository.dBW_Mst_DPRs().ToList();
                return View(masterModel);
            }
            else
            {
                ViewBag.DprDtl = applicationRepository.dBW_Mst_DPRs().ToList();
                return View();
            }
        }

        [HttpPost]
        public ActionResult DBW_DPR_Master(MasterModel DprMasterModel)
        {
            int count = applicationRepository.SaveDBWMstDPR(DprMasterModel);
            if (count == 1)
            {
                TempData["Message"] = "DPR Estimate Added Successfully";
            }
            else
            {
                TempData["Message"] = "DPR Estimate Updated Successfully";
            }
            return RedirectToAction("DBW_DPR_Master", "Admin");
        }

        //public ActionResult Delete_DBW_DPR_Master(int id)
        //{
        //    var data = applicationRepository.Delete_DBW_DPR_Master(id);
        //    if (data == true)
        //    {
        //        TempData["WarningMessage"] = "DPR Estimate Deleted Successfully";
        //    }
        //    else
        //    {
        //        TempData["WarningMessage"] = "Something went wrong..!";
        //    }
        //    return RedirectToAction("DBW_DPR_Master", "Admin");
        //}

        public ActionResult DPREstimation()
        {
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
            return View();
        }

        public class CalculatioModel
        {
            public double total { get; set; }
            public double? subtotal { get; set; }
            public double? grandtotal { get; set; }

        }

        public ActionResult SDBW_DPR_Master()
        {
            return View();
        }

        public ActionResult CLIP_DPR_Master()
        {
            return View();
        }


        //HCM
        public ActionResult HCM_EmployeeDtls()
        {
            ViewBag.EmpDtls = applicationRepository.hcm_EmployeeDtls().ToList();
            return View();
        }


        //[HttpGet]
        //public async Task<ActionResult> UpdateHcmEmployees()
        //{
        //try
        //{
        //    //if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 1)
        //    //{
        //    string Baseurl = Security.sapurl;
        //    string token = Security.sapu_sername + ":" + Security.sap_password;

        //    List<HCM_EmployeeDtl> hcm_EmployeeDtls = new List<HCM_EmployeeDtl>();

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(Baseurl);

        //        var byteArray = Encoding.ASCII.GetBytes(token);
        //        var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        //        client.DefaultRequestHeaders.Authorization = header;

        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage Res = await client.GetAsync("sap/bc/bottle_unit?sap-client=130");
        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var EmpResponse = Res.Content.ReadAsStringAsync().Result;
        //            hcm_EmployeeDtls = JsonConvert.DeserializeObject<List<HCM_EmployeeDtl>>(EmpResponse);
        //        }

        //        return RedirectToAction("HCM_EmployeeDtls", "Admin");

        //        //if (hcm_EmployeeDtls.ToList().Count > 0)
        //        //{
        //        //    var data = applicationRepository.hcm_EmployeeDtls().ToList();

        //        //    if (data == 1)
        //        //    {
        //        //        TempData["Message"] = "HCM Employee list updated successfully";
        //        //        return RedirectToAction("HCM_EmployeeDtls", "Admin");
        //        //    }
        //        //    else
        //        //    {
        //        //        TempData["WarningMessage"] = "Something went wrong";
        //        //        return RedirectToAction("HCM_EmployeeDtls", "Admin");
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    TempData["WarningMessage"] = "There is no item available for updation";
        //        //    return RedirectToAction("HCM_EmployeeDtls", "Admin");
        //        //}
        //    }
        //    //}
        //    //else
        //    //{
        //    //    return RedirectToAction("Login", "Home");
        //    //}
        //}
        //catch (Exception ex)
        //{
        //    return RedirectToAction("Login", "Home");
        //}
        //}



        //Division_Master

        
        public async Task<ActionResult> OlicMaster()
        {
            try
            {
                //if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 1)
                //{
                string Baseurl = Security.sapurl;
                string token = Security.sapu_sername + ":" + Security.sap_password;

                List<OlicMstDtl_SAP> OlicMstDtl_SAP = new List<OlicMstDtl_SAP>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    var byteArray = Encoding.ASCII.GetBytes(token);
                    var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Authorization = header;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("sap/bc/circle_details?sap-client=120");
                    if (Res.IsSuccessStatusCode)
                    {
                        var MstResponse = Res.Content.ReadAsStringAsync().Result;
                        OlicMstDtl_SAP = JsonConvert.DeserializeObject<List<OlicMstDtl_SAP>>(MstResponse);
                    }

                    if (OlicMstDtl_SAP.ToList().Count > 0)
                    {
                        var data = applicationRepository.SaveOlicMaster(OlicMstDtl_SAP);

                        if (data > 0)
                        {
                            TempData["Message"] = "Master Ditails updated successfully";
                            return RedirectToAction("OlicMaster", "Admin");
                        }
                        else
                        {
                            TempData["WarningMessage"] = "Something went wrong";
                            return RedirectToAction("OlicMaster", "Admin");
                        }
                    }
                    else
                    {
                        TempData["WarningMessage"] = "There is no item available for updation";
                        return RedirectToAction("OlicMaster", "Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Home");
            }
        }


        [HttpPost]
        public async Task<ActionResult> CircleMaster()
        {
            try
            {
                string Baseurl = Security.sapurl;
                string token = Security.sapu_sername + ":" + Security.sap_password;

                List<CircleMaster_SAP> CircleMaster_SAP = new List<CircleMaster_SAP>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    var byteArray = Encoding.ASCII.GetBytes(token);
                    var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Authorization = header;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("sap/bc/circle?sap-client=120");
                    if (Res.IsSuccessStatusCode)
                    {
                        var CircleResponse = Res.Content.ReadAsStringAsync().Result;
                        CircleMaster_SAP = JsonConvert.DeserializeObject<List<CircleMaster_SAP>>(CircleResponse);
                    }

                    if (CircleMaster_SAP.ToList().Count > 0)
                    {
                        var data = applicationRepository.SaveCircleMaster(CircleMaster_SAP);

                        if (data > 0)
                        {
                            TempData["Message"] = "Circle Ditails updated successfully";
                            return RedirectToAction("CircleMaster", "Admin");
                        }
                        else
                        {
                            TempData["WarningMessage"] = "Something went wrong";
                            return RedirectToAction("CircleMaster", "Admin");
                        }
                    }
                    else
                    {
                        TempData["WarningMessage"] = "There is no item available for updation";
                        return RedirectToAction("CircleMaster", "Admin");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult CircleMaster(int? id = 0)
        {
            if( id != 0)
            {


                return View();
            }
            else
            {
                ViewBag.CircleDtls = applicationRepository.mst_Circles().ToList();
                return View();
            }
        }


        [HttpPost]
        public async Task<ActionResult> DivisionMaster()
        {
            try
            {
                string Baseurl = Security.sapurl;
                string token = Security.sapu_sername + ":" + Security.sap_password;

                List<DivisionMaster_SAP> DivisionMaster_SAP = new List<DivisionMaster_SAP>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    var byteArray = Encoding.ASCII.GetBytes(token);
                    var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Authorization = header;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("sap/bc/division?sap-client=120");
                    if (Res.IsSuccessStatusCode)
                    {
                        var DivisionResponse = Res.Content.ReadAsStringAsync().Result;
                        DivisionMaster_SAP = JsonConvert.DeserializeObject<List<DivisionMaster_SAP>>(DivisionResponse);
                    }

                    if (DivisionMaster_SAP.ToList().Count > 0)
                    {
                        var data = applicationRepository.SaveDivisionMaster(DivisionMaster_SAP);

                        if (data > 0)
                        {
                            TempData["Message"] = "Division Ditails updated successfully";
                            return RedirectToAction("DivisionMaster", "Admin");
                        }
                        else
                        {
                            TempData["WarningMessage"] = "Something went wrong";
                            return RedirectToAction("DivisionMaster", "Admin");
                        }
                    }
                    else
                    {
                        TempData["WarningMessage"] = "There is no item available for updation";
                        return RedirectToAction("DivisionMaster", "Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult DivisionMaster(int? id = 0)
        {
            if (id != 0)
            {


                return View();
            }
            else
            {
                ViewBag.DivisionDtls = applicationRepository.mst_Divisions().ToList();
                return View();
            }
        }


        [HttpPost]
        public async Task<ActionResult> SubDivisionMaster()
        {
            try
            {
                string Baseurl = Security.sapurl;
                string token = Security.sapu_sername + ":" + Security.sap_password;

                List<SubDivisionMaster_SAP> SubDivisionMaster_SAP = new List<SubDivisionMaster_SAP>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    var byteArray = Encoding.ASCII.GetBytes(token);
                    var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Authorization = header;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("sap/bc/sub_division?sap-client=120");
                    if (Res.IsSuccessStatusCode)
                    {
                        var SubDivisionResponse = Res.Content.ReadAsStringAsync().Result;
                        SubDivisionMaster_SAP = JsonConvert.DeserializeObject<List<SubDivisionMaster_SAP>>(SubDivisionResponse);
                    }

                    if (SubDivisionMaster_SAP.ToList().Count > 0)
                    {
                        var data = applicationRepository.SaveSubDivisionMaster(SubDivisionMaster_SAP);

                        if (data > 0)
                        {
                            TempData["Message"] = "Sub-Division Ditails updated successfully";
                            return RedirectToAction("SubDivisionMaster", "Admin");
                        }
                        else
                        {
                            TempData["WarningMessage"] = "Something went wrong";
                            return RedirectToAction("SubDivisionMaster", "Admin");
                        }
                    }
                    else
                    {
                        TempData["WarningMessage"] = "There is no item available for updation";
                        return RedirectToAction("SubDivisionMaster", "Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult SubDivisionMaster(int? id = 0)
        {
            if (id != 0)
            {


                return View();
            }
            else
            {
                ViewBag.SubDivisionDtls = applicationRepository.mst_SubDivisions().ToList();
                return View();
            }
        }


        [HttpPost]
        public async Task<ActionResult> SectionMaster()
        {
            try
            {
                string Baseurl = Security.sapurl;
                string token = Security.sapu_sername + ":" + Security.sap_password;

                List<SectionMaster_SAP> SectionMaster_SAP = new List<SectionMaster_SAP>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    var byteArray = Encoding.ASCII.GetBytes(token);
                    var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Authorization = header;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("sap/bc/section?sap-client=120");

                    if (Res.IsSuccessStatusCode)
                    {
                        var SectionResponse = Res.Content.ReadAsStringAsync().Result;
                        SectionMaster_SAP = JsonConvert.DeserializeObject<List<SectionMaster_SAP>>(SectionResponse);
                    }

                    if (SectionMaster_SAP.ToList().Count > 0)
                    {
                        var data = applicationRepository.SaveSectionMaster(SectionMaster_SAP);

                        if (data > 0)
                        {
                            TempData["Message"] = "Section Ditails updated successfully";
                            return RedirectToAction("SectionMaster", "Admin");
                        }
                        else
                        {
                            TempData["WarningMessage"] = "Something went wrong";
                            return RedirectToAction("SectionMaster", "Admin");
                        }
                    }
                    else
                    {
                        TempData["WarningMessage"] = "There is no item available for updation";
                        return RedirectToAction("SectionMaster", "Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult SectionMaster(int? id = 0)
        {
            if (id != 0)
            {


                return View();
            }
            else
            {
                ViewBag.SectionDtls = applicationRepository.mst_Sections().ToList();
                return View();
            }
        }


        public ActionResult ManageUser()
        {
            return View();
        }






    }
}