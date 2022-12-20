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

namespace OLIC_Web.Controllers
{
    public class ScrapController : Controller
    {
        private readonly BLL.AbstractLayer applicationRepository;




        OLICEntities db = new OLICEntities();

        public ScrapController(BLL.AbstractLayer appRepository)
        {
            applicationRepository = appRepository;
        }

        // GET: Scrap
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ScrapDetails()
        {

            string Baseurl = Security.sapurl;
            string token = Security.sapu_sername + ":" + Security.sap_password;

            List<ScrapSAPMasterModel> ScrapSAPMasterModel = new List<ScrapSAPMasterModel>();
            List<Scrap_Iteam_master_Model> Scrap_Iteam_master_Model = new List<Scrap_Iteam_master_Model>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var byteArray = Encoding.ASCII.GetBytes(token);
                var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Authorization = header;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("sap/bc/mat_TYPE?sap-client=120");
                if (Res.IsSuccessStatusCode)
                {
                    var MstResponse = Res.Content.ReadAsStringAsync().Result;
                    ScrapSAPMasterModel = JsonConvert.DeserializeObject<List<ScrapSAPMasterModel>>(MstResponse);
                }

                HttpResponseMessage Res1 = await client.GetAsync("sap/bc/mat_details?sap-client=120");
                if (Res1.IsSuccessStatusCode)
                {
                    var MstResponse1 = Res1.Content.ReadAsStringAsync().Result;
                    Scrap_Iteam_master_Model = JsonConvert.DeserializeObject<List<Scrap_Iteam_master_Model>>(MstResponse1);
                }

                //string data22 = "MBPS-0002";
                //string data33 = "ZSRP";
                //string data44 = "1ODA";
                //HttpResponseMessage Res2 = await client.GetAsync("sap/bc/mat_id/" + data33 + "|" + data22 + "|" + data44 + "?sap-client=120");
                ////HttpResponseMessage Res2 = await client.GetAsync("sap/bc/mat_id/" + data33  + data22  + data44 + "?sap-client=120");
                //if (Res2.IsSuccessStatusCode)
                //{
                //    var MstResponse1 = Res2.Content.ReadAsStringAsync().Result;
                //    Scrap_Iteam_master_Model = JsonConvert.DeserializeObject<List<Scrap_Iteam_master_Model>>(MstResponse1);
                //}








                applicationRepository.Savescrap_Category(ScrapSAPMasterModel);
                applicationRepository.Scrap_Item_Mst(Scrap_Iteam_master_Model);
            }


            var scrap_category = db.Scrap_Catrgory_Mst.Where(x => x.IsActive == true).ToList();


            List<SelectListItem> li1 = new List<SelectListItem>();
            li1.Add(new SelectListItem { Text = "--Select--", Value = "0", });
            if (scrap_category.ToList().Count > 0)
            {
                foreach (var itema in scrap_category)
                {
                    li1.Add(new SelectListItem
                    {
                        Text = itema.Category_Name,
                        Value = itema.Scrap_Category_Id.ToString(),
                    });
                }


            }

            ViewBag.typescrap = li1;

            var iteam = db.Scrap_Item_Mst.Where(x => x.IsActive == true).ToList();


            List<SelectListItem> li2 = new List<SelectListItem>();
            li2.Add(new SelectListItem { Text = "--Select--", Value = "0", });
            if (iteam.ToList().Count > 0)
            {
                foreach (var itema in iteam)
                {
                    li2.Add(new SelectListItem
                    {
                        Text = itema.Iteam_name,
                        Value = itema.Id.ToString(),
                    });
                }


            }

            ViewBag.Iteam = li2;




            return View();
        }







        [HttpPost]
        public async Task<ActionResult> ScrapDetails(string[] Scrap_Category_Id, int[] Scrap_Iteam_Id, string[] rate, string[] amount, string[] aw,
            string[] tw, string[] dor, string[] no_of_scrap, string Remark)
        {
            Scrap_Details_Entry_MainModel scrap = new Scrap_Details_Entry_MainModel();

            List<Scrap_Details> sdetail = new List<Scrap_Details>();

            if (Scrap_Category_Id != null)
            {
                for (int i = 0; i < Scrap_Category_Id.Length; i++)
                {
                    Scrap_Details p = new Scrap_Details();
                    p.Scrap_Category_Id=Convert.ToInt32(Scrap_Category_Id[i]);
                    p.Scrap_Iteam_Id = Scrap_Iteam_Id[i];
                    p.rate= rate[i];
                    p.amount = amount[i];
                    p.aw = aw[i];
                    p.tw = tw[i];
                    p.dor =Convert.ToDateTime( dor[i]);
                    p.no_of_scrap = no_of_scrap[i];
            
                    sdetail.Add(p);
                    p = new Scrap_Details();
                }
            }
            scrap.Scrap_Details = sdetail.ToList();
            scrap.Remark = Remark;
         int count=applicationRepository.Scrap_Details_Save(scrap);
            if(count==1)
            {
                TempData["msg"] = "Scrap Saved Successfully";
            }



            return RedirectToAction("ScrapDetails");
        }



        [HttpPost]
        public async Task<ActionResult> GetScrap_Iteam_FromCategory(int Scrap_Category_Id)
        {
            var a = db.Scrap_Catrgory_Mst.Where(x => x.Scrap_Category_Id == Scrap_Category_Id).FirstOrDefault();
            //HaziraEntities schoolEntity = new HaziraEntities();
            var data = db.Scrap_Item_Mst.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            int dId;
            List<SelectListItem> scrap_Iteam = new List<SelectListItem>();

            dId = Convert.ToInt32(Scrap_Category_Id);
            List<Scrap_Item_Mst> Scrap_Item_Mstt = data.Where(x => x.MaterialType == a.MaterialType).ToList();
            Scrap_Item_Mstt.ForEach(x =>

            {

                scrap_Iteam.Add(new SelectListItem { Text = x.Iteam_name.ToString(), Value = x.Id.ToString() });

            });




            return Json(scrap_Iteam);
        }


        [HttpPost]
        public JsonResult GetSAPcode(int typescrap,int material)
        {
            Session["usertypee"] = "1ODA";
            Get_Sap_Code_fROM_Table rr = new Get_Sap_Code_fROM_Table();
            var ss = db.Scrap_Catrgory_Mst.Where(x => x.Scrap_Category_Id== typescrap).FirstOrDefault();
            rr.MaterialType = ss.MaterialType;
            var yy = db.Scrap_Item_Mst.Where(x => x.Id == material).FirstOrDefault();
            rr.Matreial_Code = yy.Matreial_Code;
            rr.User_Type = Session["usertypee"].ToString();
            return Json(rr);
        }


        public async Task<ActionResult> GetSAPQuantity(string MaterialType,string Matreial_Code,string User_Type)
        {
            string Baseurl = Security.sapurl;
            string token = Security.sapu_sername + ":" + Security.sap_password;

            List<ScrapSAPMasterModel> ScrapSAPMasterModel = new List<ScrapSAPMasterModel>();
            List<Get_Sap_Code_fROM_Table> Get_Sap_Code_fROM_Table = new List<Get_Sap_Code_fROM_Table>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var byteArray = Encoding.ASCII.GetBytes(token);
                var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Authorization = header;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res2 = await client.GetAsync("sap/bc/mat_id/" + MaterialType + "|" + Matreial_Code + "|" + User_Type + "?sap-client=120");
                //HttpResponseMessage Res2 = await client.GetAsync("sap/bc/mat_id/" + data33  + data22  + data44 + "?sap-client=120");
                if (Res2.IsSuccessStatusCode)
                {
                    var MstResponse1 = Res2.Content.ReadAsStringAsync().Result;
                    Get_Sap_Code_fROM_Table = JsonConvert.DeserializeObject<List<Get_Sap_Code_fROM_Table>>(MstResponse1);
                }

            }


            return Json(Get_Sap_Code_fROM_Table);
        }
   
    
        public ActionResult Scrap_List()
        {
            var data = db.Scrap_code.ToList();
            ViewBag.Scrapcode = data;

            return View();
        }
    

        public ActionResult Scrap_List_Details(int id)
        {
            Scrap_Details_Entry_MainModel model = new Scrap_Details_Entry_MainModel();
            model.sp_Scrap_Detail_Category_Results = db.sp_scrap_detail_category(id).ToList();
            model.sp_Scrap_Item_Detail_Category_Wise_Results = db.sp_scrap_item_detail_category_wise(id).ToList();
            int data1 = db.sp_scrap_item_detail_category_wise(id).ToList().Count;
            int data2 = db.sp_scrap_detail_category(id).ToList().Count;

            model.rowcount = data1+data2;
            ViewBag.Remark = db.Scrap_code.Where(x => x.Scrap_Code_Autogenerate_Id == id).Select(y => y.Remark).FirstOrDefault();
            return View(model);
        }



    } }
