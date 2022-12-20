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
    public class HcmController : Controller
    {
        // GET: Hcm
        private readonly BLL.AbstractLayer applicationRepository;

        OLICEntities db = new OLICEntities();


        public HcmController(BLL.AbstractLayer apprepo)
        {
            applicationRepository = apprepo;
        }

        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult Dashboard(string uname)
        //{
        //    //uname=(string)Session["username"];
        //    uname = (string)TempData["username"];
        //    TempData["username"] = uname;
        //    return View();
        

        public ActionResult Dashboard()
        {

            return View();
        }

        //Leave Section

        public ActionResult ApplyLeave()
        {
            try
            {
               
                ApplyLeaveModel nnh = new ApplyLeaveModel();
                string uid= Session["usercode"].ToString();
                if (uid != null)
                {

                    nnh.getleavetype = applicationRepository.Leavetypelist();
                    //string uname=null;
                    //Session["userid"] = uname;
                    //ViewBag.LeaveDtls = applicationRepository.hcm_LeaveMasterDtls();
                    ViewBag.LeaveDtls = applicationRepository.hcm_LeaveMasterDtls(uid).ToList();
                    return View(nnh);   
                }

                nnh.getleavetype = applicationRepository.Leavetypelist();
                return View(nnh);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult ApplyLeave(ApplyLeaveModel nh)
        {
            try
            {

                int id = Convert.ToInt32(Session["userid"]);
                string idd = Session["usercode"].ToString();
                nh.applyLeavelr.EmployeeID = id;

                nh.applyLeavelr.EmployeeCode = idd;
                if (Session["userid"] != null)
                {
                    int count = applicationRepository.saveapplyleave(nh);

                    if (count == 1)
                    {
                        TempData["Message"] = "Apply Leave Completed Successfully";
                    }
                    else
                    {
                        TempData["Message"] = "Leave Already Exist";
                    }
                    return RedirectToAction("View_CancelLeave", "Hcm");

                }
                else
                {
                    return RedirectToAction("ApplyLeave", "Hcm");
                }
            }

            catch(Exception ex)
            {
                throw;
            }
        }



        [HttpGet]
      
        public ActionResult ShowLeaveDetails()

        {
            //ApplyLeave employee = db.ApplyLeaves.Where(e => e.EmployeeID == id).FirstOrDefault();
            //ViewBag.LeaveDetails = db.ApplyLeaves.Where(e => e.EmployeeID == id).FirstOrDefault();
           // ViewBag.LeaveDetails = applicationRepository.hcm_LeaveDetails().ToList();
          
            return View();

            //return View();
        }
        public ActionResult LeaveDetails()
        {

            
            //ViewBag.leaveDetails = applicationRepository.().ToList();
           // ViewBag.LeaveDetailss = applicationRepository.().ToList();
           
            return View();
        }
        public ActionResult AddProperty()
        {
           
            return View();
        }

        [HttpGet]
        public ActionResult EditPropertyDetails()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult EditPropertyDetails(EmployeePropertyDetails employeePropertyDetails,int[] Id, string[] PreciseLocation, string[] ExtentOInterest, string Value, string[] InWhoseName, DateTime[] DateAndmanner, string[] Precise, string[] Extent, int[] Valuee, string[] Inwhose, DateTime[] DateManner, string[] IBreif, string[] Iextent, int[] IValue, string[] IInWhoseName, DateTime[] IDateAndmanner, string[] IRemark, string[] MDes, int[] MVal, string[] MInwhose, DateTime[] MDate, string[] MLoans, string[] MRemarks, string[] OMDes, int[] OMVal, string[] OMInwhose, DateTime[] OMDate, string[] OMRemarks)
        
        {
           

              
            if (employeePropertyDetails == null)
            {
                return RedirectToAction("Details", "Hcm");
            }

            return RedirectToAction("Details", "Hcm");

            //return View();
        }

        public ActionResult PropertyDetails(int id )

        {
            Hcm_Property_Land employee = db.Hcm_Property_Land.Where(e => e.Id == id).FirstOrDefault();
            return View(employee);

            //return View();
        }
        public ActionResult PropertyDetails1(int id)

        {
            Hcm_Property_House employee = db.Hcm_Property_House.Where(e => e.Id == id).FirstOrDefault();
            return View(employee);

            //return View();
        }
        public ActionResult PropertyDetails2(int id)

        {
            Hcm_Property_ImmovableProperties employee = db.Hcm_Property_ImmovableProperties.Where(e => e.Id == id).FirstOrDefault();
            return View(employee);

            //return View();
        }
        public ActionResult PropertyDetails3(int id)

        {
            Hcm_Property_MovableProperty employee = db.Hcm_Property_MovableProperty.Where(e => e.Id == id).FirstOrDefault();
            return View(employee);

            //return View();
        }
        public ActionResult PropertyDetails4(int id)

        {
            Hcm_Property_OtherMovable employee = db.Hcm_Property_OtherMovable.Where(e => e.Id == id).FirstOrDefault();
            return View(employee);

            //return View();
        }

        [HttpGet]
        public ActionResult Details()
        {

            string uid = Session["usercode"].ToString();
            ViewBag.LandDtls = applicationRepository.hcm_LandDtls(uid).ToList();
            ViewBag.HouseDtls = applicationRepository.hcm_HouseDtls(uid).ToList();
            ViewBag.ImmovableProperty = applicationRepository.hcm_ImmovablepropertyDtls(uid).ToList();
            ViewBag.MovableProperty = applicationRepository.hcm_MovablepropertyDtls(uid).ToList();
            ViewBag.OmovableProperty = applicationRepository.hcm_OMovablepropertyDtls(uid).ToList();
            // ViewBag.LandDtls = applicationRepository.hcm_LandDtls().ToList();
            //ViewBag.HouseDtls = applicationRepository.hcm_HouseDtls().ToList();
            //ViewBag.ImmovableProperty = applicationRepository.hcm_ImmovablepropertyDtls().ToList();
            // ViewBag.MovableProperty = applicationRepository.hcm_MovablepropertyDtls().ToList();
            // ViewBag.OmovableProperty = applicationRepository.hcm_OMovablepropertyDtls().ToList();
            return View();

        }
        [HttpPost]
        public ActionResult Details(EmployeePropertyDetails employeePropertyDetails,  int[] Id, string[] PreciseLocation, string[] ExtentOInterest, int[] Value, string[] InWhoseName,
            DateTime[] DateAndmanner, int[] HId, string[] Precise, string[] Extent, int[] Valuee, string[] Inwhose,
            DateTime[] DateManner, int[] IId, string[] IBreif, string[] Iextent, int[] IValue, string[] IInWhoseName,
            DateTime[] IDateAndmanner, string[] IRemark,int[] MId, string[] MDes, int[] MVal, string[] MInwhose, 
            DateTime[] MDate, string[] MLoans, string[] MRemarks,int[] OMId, string[] OMDes, int[] OMVal,
            string[] OMInwhose, DateTime[] OMDate, string[] OMRemarks,HttpPostedFileBase[] fille,string [] fille1, HttpPostedFileBase[] fillee, string[] fille2, HttpPostedFileBase[] filleee, string[] fille3, HttpPostedFileBase[] filleeee, string[] fille4, HttpPostedFileBase[] filleeeee, string[] fille5
            )
        {


            List<Landss> pdetail = new List<Landss>();
            List<Houses> hdetails = new List<Houses>();
            List<Immovableproperties> immodetails = new List<Immovableproperties>();
            List<MovableProperties> movableProperties = new List<MovableProperties>();
            List<Othermovables> othermovables = new List<Othermovables>();

            ViewBag.LandDtls = applicationRepository.hcm_LandDtls().ToList();
            ViewBag.HouseDtls = applicationRepository.hcm_HouseDtls().ToList();
            ViewBag.ImmovableProperty = applicationRepository.hcm_ImmovablepropertyDtls().ToList();
            ViewBag.MovableProperty = applicationRepository.hcm_MovablepropertyDtls().ToList();
            ViewBag.OmovableProperty = applicationRepository.hcm_OMovablepropertyDtls().ToList();
            string fileloc = "/FileUploadFolder/";
            string uid = Session["usercode"].ToString();

            //if (Id != null)
            //{
            //    foreach (var permission in ViewBag.LandDtls)
            //    {
            //        if ( permission.Id !=Id)
            //        {

            //        }
            //    }
            //}



            if (PreciseLocation[0] != "")
            {

                for (int i = 0; i < PreciseLocation.Length; i++)
                {
   
                        Landss q = new Landss();

                        q.Id = Id[i];
                        q.PreciseLocation = PreciseLocation[i];
                        q.ExtentOfInterest = ExtentOInterest[i];
                        q.InWhoseName = InWhoseName[i];
                        q.Value = Value[i];
                        q.EmpCode = uid;
                        q.DateAndMannerOfAcquisitionOrDisposal = DateAndmanner[i];
                        //q.Files = fille[i].ToString();
                        if(fille[i] != null)
                        {
                            var Adharrr = UploadFileHelper.Benificiary_SaveFileIntoLocal(fille[i], FileTypes.AadhaarFile);
                            // EmployeePropertyDetails.DB_RegistrationLand.Files = fileloc + Adharr.SystemFileName;
                            q.Files = fileloc + Adharrr.SystemFileName;
                        }
                        else
                        {
                            q.Files = fille1[i];
                        }
                        //q.Khata = Khat=a[i];
                        //q.Land = Land[i];
                        //q.Plot = Plot[i];
                        //q.Patta = fileloc + bhg.SystemFileName;
                        pdetail.Add(q);
                        q = new Landss();

                    
                }
            }
        
            if (Precise[0] != "")
            {
                for (int i = 0; i < Precise.Length; i++)
                {
                   
                        Houses h = new Houses();
                        h.Id = HId[i];
                        h.PreciseLocation = Precise[i];
                        h.ExtentLOfInterest = Extent[i];
                        h.InWhoseName = Inwhose[i];
                        h.Value = Valuee[i];
                        h.DateAndManner = DateManner[i];
                        h.EmpCode = uid;
                        if (fillee[i] != null  )
                        {
                            var Adharrr = UploadFileHelper.Benificiary_SaveFileIntoLocal(fillee[i], FileTypes.AadhaarFile);
                            // EmployeePropertyDetails.DB_RegistrationLand.Files = fileloc + Adharr.SystemFileName;
                            h.Files = fileloc + Adharrr.SystemFileName;
                        }
                        else
                        {
                            h.Files = fille2[i];
                        }
                        hdetails.Add(h);
                        h = new Houses();
                    
                }
            }

            if (IBreif[0] != "")
            {
                for (int i = 0; i < IBreif.Length; i++)
                {
                   

                        Immovableproperties m = new Immovableproperties();
                        m.Id = IId[i];
                        m.BreifDescription = IBreif[i];
                        m.ExtentOfInterest = Iextent[i];
                        m.InWhoseName = IInWhoseName[i];
                        m.Value = IValue[i];
                        m.DateAndMannerOfAcquisitionOrDiposal = IDateAndmanner[i];
                        m.Remarks = IRemark[i];
                        m.EmpCode = uid;
                        if (filleee[i] != null)
                        {
                            var Adharrr = UploadFileHelper.Benificiary_SaveFileIntoLocal(filleee[i], FileTypes.AadhaarFile);
                            // EmployeePropertyDetails.DB_RegistrationLand.Files = fileloc + Adharr.SystemFileName;
                            m.Files = fileloc + Adharrr.SystemFileName;
                        }
                        else
                        {
                            m.Files = fille3[i];
                        }
                        //q.Khata = Khata[i];
                        //q.Land = Land[i];
                        //q.Plot = Plot[i];
                        //q.Patta = fileloc + bhg.SystemFileName;
                        immodetails.Add(m);
                        m = new Immovableproperties();
                   
                }
            }

         
           

            if (MDes[0] != "")
            {
                for (int i = 0; i < MDes.Length; i++)
                {
                   
                   
                        MovableProperties mo = new MovableProperties();
                        mo.Id = MId[i];
                        mo.DescriptionOfItems = MDes[i];
                        mo.InWhoseName = MInwhose[i];
                        mo.Value = MVal[i];
                        mo.Loans = MLoans[i];
                        mo.Remarks = MRemarks[i];
                        mo.DateAndManner = MDate[i];
                        mo.EmpCode = uid;
                        //q.Khata = Khata[i];
                        //q.Land = Land[i];
                        //q.Plot = Plot[i];
                        //q.Patta = fileloc + bhg.SystemFileName;
                        if (filleeee[i] != null)
                        {
                            var Adharrr = UploadFileHelper.Benificiary_SaveFileIntoLocal(filleeee[i], FileTypes.AadhaarFile);
                            // EmployeePropertyDetails.DB_RegistrationLand.Files = fileloc + Adharr.SystemFileName;
                            mo.Files = fileloc + Adharrr.SystemFileName;
                        }
                        else
                        {
                            mo.Files = fille4[i];
                        }
                        movableProperties.Add(mo);
                        mo = new MovableProperties();
                   
                }
            }


             //public int Id { get; set; }
        //public string DescriptionOfItems { get; set; }
        //public Nullable<int> Value { get; set; }
        //public string InWhoseName { get; set; }
        //public Nullable<System.DateTime> DateAndManner { get; set; }
        //public string Remarks { get; set; }
            if (OMDes[0] != "")
            {
                for (int i = 0; i < OMDes.Length; i++)
                {
                    
                        Othermovables omo = new Othermovables();
                        omo.Id = OMId[i];
                        omo.DescriptionOfItems = OMDes[i];
                        omo.InWhoseName = OMInwhose[i];
                        omo.Value = OMVal[i];
                        omo.Remarks = OMRemarks[i];
                        omo.DateAndManner = OMDate[i];
                        omo.EmpCode = uid;
                        //mo.DescriptionOfItems = MDes[i];
                        //mo.InWhoseName = MInwhose[i];
                        //mo.Value = MVal[i];
                        //mo.Loans = MLoans[i];
                        //mo.Remarks = MRemarks[i];
                        //q.Khata = Khata[i];
                        //q.Land = Land[i];
                        //q.Plot = Plot[i];
                        //q.Patta = fileloc + bhg.SystemFileName;
                        if (filleeeee[i] != null)
                        {
                            var Adharrr = UploadFileHelper.Benificiary_SaveFileIntoLocal(filleeeee[i], FileTypes.AadhaarFile);
                            // EmployeePropertyDetails.DB_RegistrationLand.Files = fileloc + Adharr.SystemFileName;
                            omo.Files = fileloc + Adharrr.SystemFileName;
                        }
                        else
                        {
                            omo.Files = fille5[i];
                        }
                        othermovables.Add(omo);
                        omo = new Othermovables();
                   
                }
            }

            employeePropertyDetails.ListOfLands = pdetail.ToList();
            employeePropertyDetails.ListOfHouses = hdetails.ToList();
            employeePropertyDetails.ListOfImmovableproperties = immodetails.ToList();
            employeePropertyDetails.ListOfMovableproperties = movableProperties.ToList();
            employeePropertyDetails.ListOfOthermovables = othermovables.ToList();




            int count = applicationRepository.EditHcmEmp(employeePropertyDetails);

            return RedirectToAction("AddProperty", "Hcm");
            //return View();
        }

        [HttpGet]
        public ActionResult ShowPropertyDetails()
        {

            string uid = Session["usercode"].ToString();
            ViewBag.LandDtls = applicationRepository.hcm_LandDtls(uid).ToList();
            ViewBag.HouseDtls = applicationRepository.hcm_HouseDtls(uid).ToList();
            ViewBag.ImmovableProperty = applicationRepository.hcm_ImmovablepropertyDtls(uid).ToList();
            ViewBag.MovableProperty = applicationRepository.hcm_MovablepropertyDtls(uid).ToList();
            ViewBag.OmovableProperty= applicationRepository.hcm_OMovablepropertyDtls(uid).ToList();
            //ViewBag.PropertyDetails = applicationRepository.hcm_PropertyDtls();
            return View();
            
        }

      
        [HttpPost]
        public ActionResult ShowPropertyDetails(EmployeePropertyDetails employeePropertyDetails, string[] PreciseLocation, string[] ExtentOInterest, int[] Value,
            string[] InWhoseName,DateTime[] DateAndmanner, HttpPostedFileBase[] Adhar1, string[] Precise,
            string[] Extent,int[] Valuee,string[] Inwhose,DateTime[] DateManner,HttpPostedFileBase[] Adhar2,
            string[] IBreif,string[] Iextent,int[] IValue,string[] IInWhoseName,DateTime[] IDateAndmanner,HttpPostedFileBase[] Adhar3,string[] IRemark,string[] MDes,
            int[] MVal,string[] MInwhose,DateTime[] MDate,string[] MLoans, HttpPostedFileBase[] Adhar4,string[] MRemarks,string[] OMDes,int[] OMVal,string[] OMInwhose,
            DateTime[] OMDate, HttpPostedFileBase[] Adhar5,string[] OMRemarks)
        {

           
            List<Landss> pdetail = new List<Landss>();
            List<Houses> hdetails = new List<Houses>();
            List<Immovableproperties> immodetails = new List<Immovableproperties>();
            List<MovableProperties> movableProperties = new List<MovableProperties>();
            List<Othermovables> othermovables = new List<Othermovables>();

            string fileloc = "/FileUploadFolder/";
            string uid = Session["usercode"].ToString();
            if (PreciseLocation[0] !="")
            {
                for (int i = 0; i < PreciseLocation.Length; i++)
                {
                    Landss q = new Landss();

                    q.PreciseLocation = PreciseLocation[i];
                    q.ExtentOfInterest = ExtentOInterest[i];
                    q.InWhoseName = InWhoseName[i];
                    q.Value = Value[i];
                    q.DateAndMannerOfAcquisitionOrDisposal = DateAndmanner[i];
                    q.EmpCode = uid;

                    if (Adhar1 != null)
                    {
                        var Adharr = UploadFileHelper.Benificiary_SaveFileIntoLocal(Adhar1[i], FileTypes.AadhaarFile);
                        // EmployeePropertyDetails.DB_RegistrationLand.Files = fileloc + Adharr.SystemFileName;
                        q.Files = fileloc + Adharr.SystemFileName;
                    }
                    //q.Files = fileloc+ Adhar1[i].FileName;
                    //q.Khata = Khata[i];
                    //q.Land = Land[i];
                    //q.Plot = Plot[i];
                    //q.Patta = fileloc + bhg.SystemFileName;
                    pdetail.Add(q);
                    q = new Landss();
                }
            }


            if (Precise[0] !="")
            {
                for (int i = 0; i < Precise.Length; i++)
                {
                    Houses h = new Houses();

                    h.PreciseLocation = Precise[i];
                    h.ExtentLOfInterest = Extent[i];
                    h.InWhoseName = Inwhose[i];
                    h.Value = Valuee[i];
                    h.DateAndManner = DateManner[i];
                    h.EmpCode = uid;
                    if (Adhar2 != null)
                    {
                        var Adharr = UploadFileHelper.Benificiary_SaveFileIntoLocal(Adhar2[i], FileTypes.AadhaarFile);
                        // EmployeePropertyDetails.DB_RegistrationLand.Files = fileloc + Adharr.SystemFileName;
                        h.Files = fileloc + Adharr.SystemFileName;
                    }
                    //h.Files = fileloc+Adhar2[i].FileName;
                    //q.Khata = Khata[i];
                    //q.Land = Land[i];
                    //q.Plot = Plot[i];
                    //q.Patta = fileloc + bhg.SystemFileName;
                    hdetails.Add(h);
                    h = new Houses();
                }
            }

            if (IBreif[0] != "")
            {
                for (int i = 0; i < IBreif.Length; i++)
                {
                    Immovableproperties m = new Immovableproperties();
                    m.BreifDescription = IBreif[i];
                    m.ExtentOfInterest = Iextent[i];
                    m.InWhoseName = IInWhoseName[i];
                    m.Value = IValue[i];
                    m.DateAndMannerOfAcquisitionOrDiposal = IDateAndmanner[i];
                    m.Remarks = IRemark[i];
                    m.EmpCode = uid;
                    if (Adhar3 != null)
                    {
                        var Adharr = UploadFileHelper.Benificiary_SaveFileIntoLocal(Adhar3[i], FileTypes.AadhaarFile);
                        // EmployeePropertyDetails.DB_RegistrationLand.Files = fileloc + Adharr.SystemFileName;
                        m.Files = fileloc + Adharr.SystemFileName;
                    }
                    //m.FileName = fileloc+Adhar3[i].FileName;
                    //q.Khata = Khata[i];
                    //q.Land = Land[i];
                    //q.Plot = Plot[i];
                    //q.Patta = fileloc + bhg.SystemFileName;
                    immodetails.Add(m);
                    m = new Immovableproperties();
                }
            }



            if (MDes[0] != "")
            {
                for (int i = 0; i < MDes.Length; i++)
                {
                    MovableProperties mo = new MovableProperties();
                    mo.DescriptionOfItems = MDes[i];
                    mo.InWhoseName = MInwhose[i];
                    mo.Value = MVal[i];
                    mo.Loans = MLoans[i];
                    mo.Remarks = MRemarks[i];
                    mo.DateAndManner = MDate[i];
                    mo.EmpCode = uid;
                    if (Adhar4 != null)
                    {
                        var Adharr = UploadFileHelper.Benificiary_SaveFileIntoLocal(Adhar4[i], FileTypes.AadhaarFile);
                        // EmployeePropertyDetails.DB_RegistrationLand.Files = fileloc + Adharr.SystemFileName;
                        mo.Files = fileloc + Adharr.SystemFileName;
                    }
                    //mo.Filename = fileloc+Adhar4[i].FileName;
                    //q.Khata = Khata[i];
                    //q.Land = Land[i];
                    //q.Plot = Plot[i];
                    //q.Patta = fileloc + bhg.SystemFileName;
                    movableProperties.Add(mo);
                    mo = new MovableProperties();
                }
            }



            if (OMDes[0] != "")
            {
                for (int i = 0; i < OMDes.Length; i++)
                {
                    Othermovables omo = new Othermovables();
                    omo.DescriptionOfItems = OMDes[i];
                    omo.InWhoseName = OMInwhose[i];
                    omo.Value = OMVal[i];
                    omo.Remarks = OMRemarks[i];
                    omo.DateAndManner = OMDate[i];
                    omo.EmpCode = uid;
                    if (Adhar5 != null)
                    {
                        var Adharr = UploadFileHelper.Benificiary_SaveFileIntoLocal(Adhar5[i], FileTypes.AadhaarFile);
                        omo.Files = fileloc + Adharr.SystemFileName;
                    }
                    //omo.Filename = fileloc+Adhar5[i].FileName;
                    //mo.DescriptionOfItems = MDes[i];
                    //mo.InWhoseName = MInwhose[i];
                    //mo.Value = MVal[i];
                    //mo.Loans = MLoans[i];
                    //mo.Remarks = MRemarks[i];
                    //q.Khata = Khata[i];
                    //q.Land = Land[i];
                    //q.Plot = Plot[i];
                    //q.Patta = fileloc + bhg.SystemFileName;
                    othermovables.Add(omo);
                    omo = new Othermovables();
                }
            }

            employeePropertyDetails.ListOfLands = pdetail.ToList();
            employeePropertyDetails.ListOfHouses = hdetails.ToList();
            employeePropertyDetails.ListOfImmovableproperties = immodetails.ToList();
            employeePropertyDetails.ListOfMovableproperties = movableProperties.ToList();
            employeePropertyDetails.ListOfOthermovables = othermovables.ToList();




            int count = applicationRepository.SaveHcmEmp(employeePropertyDetails);

            return RedirectToAction("AddProperty", "Hcm");
            //return View();
        }
        [HttpGet]
        public ActionResult View_CancelLeave()
        
        {
            try
            {
                
               //string id = Session["usercode"].ToString();
                int id = Convert.ToInt32(Session["usercode"]);
                var data= applicationRepository.hcm_LeaveDetails(id);
                ViewBag.LeaveDetails = data;
                return View();



            }
            catch (Exception ex)
            {
                throw;
            }


        }

        [HttpGet]
        public ActionResult PendingForApproval()
        {
            try
            {  

                //string idd = Session["usercode"].ToString();
                int  idd = Convert .ToInt32(Session["userid"]);
                var data = applicationRepository.LeaveDetailss(idd);

                ViewBag.LeaveDetails = data;
                return View();



            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        public ActionResult PendingForApproval(int? Id)
        {
            try
            {

                int idd = Convert.ToInt32(Session["userid"]);
                //string idd = Session["usercode"].ToString();
                var data = applicationRepository.LeaveDetailss(idd);

                ViewBag.LeaveDetails = data;
                return View("_Leave");



            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult ViewDetails(int id)
        {
            var data = applicationRepository.LeaveDetailsssssss().Where(x => x.Leave_Id == id).FirstOrDefault();
            return PartialView("_Leave", data);
        }
        [HttpPost]
        public ActionResult ResolveComplain(int id, string remark)
        {
            int Regid = Convert.ToInt32(Session["userid"]);

            int compid = Convert.ToInt32(id)
;
            return Json(applicationRepository.Resolve(Regid, compid, remark), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RejectComplain(int id, string remark)
        {
            int Regid = Convert.ToInt32(Session["userid"]);

            int compid = Convert.ToInt32(id)
;
            return Json(applicationRepository.Reject(Regid, compid, remark), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewAppliedLeaves()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SalarySlip()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SalarySlip(string month, string year)
        {
            try
            {
                var empcode = Convert.ToInt32(Session["userid"]);
                var data = db.HCM_EmployeeDtl.Where(x => x.Employee_Id == empcode).FirstOrDefault();
                string Baseurl = Security.sapurl;
                string token = Security.sapu_sername + ":" + Security.sap_password;

                List<PaySlip> PaySlip = new List<PaySlip>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    var byteArray = Encoding.ASCII.GetBytes(token);
                    var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Authorization = header;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("sap/bc/hr_payslip/"+ empcode + "|"+month+"|"+year+"?sap-client=120");
                    if (Res.IsSuccessStatusCode)
                    {
                        var MstResponse = Res.Content.ReadAsStringAsync().Result;
                        PaySlip = JsonConvert.DeserializeObject<List<PaySlip>>(MstResponse);
                    }

                    ViewBag.empcode = data.Employee_Code;
                    ViewBag.Desg = data.Designation;
                    ViewBag.Bank = data.Bank_Name;
                    ViewBag.ifsc = data.IFSC;
                    ViewBag.AccNo = data.Ac_No;
                    ViewBag.Uan = data.UAN;
                    ViewBag.Doj = data.DOJ;
                    ViewBag.Pan = data.PAN;
                    ViewBag.GisNo = data.GIS_No;
                    ViewBag.BillNo = data.Bill_No;
                    ViewBag.PfNo = data.PF_No;


                    ViewBag.name = PaySlip[0].name;
                    ViewBag.basicPay = PaySlip[0].basicPay;
                    ViewBag.dearnessAllowance = PaySlip[0].dearnessAllowance;
                    ViewBag.hra = PaySlip[0].hra;
                    ViewBag.medicalAllowance = PaySlip[0].medicalAllowance;
                    ViewBag.conveyanceAllowance = PaySlip[0].conveyanceAllowance;
                    ViewBag.specialPay = PaySlip[0].specialPay;
                    ViewBag.interimAllowance = PaySlip[0].interimAllowance;
                    ViewBag.odiaAllowance = PaySlip[0].odiaAllowance;
                    ViewBag.washingAllowance = PaySlip[0].washingAllowance;
                    ViewBag.incentiveAllowance = PaySlip[0].incentiveAllowance;
                    ViewBag.gradePay = PaySlip[0].gradePay;
                    ViewBag.motercycleAllowance = PaySlip[0].motercycleAllowance;
                    ViewBag.incomeTax = PaySlip[0].incomeTax;
                    ViewBag.pTax = PaySlip[0].pTax;
                    ViewBag.gpf = PaySlip[0].gpf;
                    ViewBag.epf = PaySlip[0].epf;
                    ViewBag.advancePf = PaySlip[0].advancePf;
                    ViewBag.quaterRent = PaySlip[0].quaterRent;
                    ViewBag.vehicleCharges = PaySlip[0].vehicleCharges;
                    ViewBag.medicalAdvance = PaySlip[0].medicalAdvance;
                    ViewBag.festivalAdvance = PaySlip[0].festivalAdvance;
                    ViewBag.mopedLoan = PaySlip[0].mopedLoan;
                    ViewBag.carLoan = PaySlip[0].carLoan;
                    ViewBag.motercycleLoan = PaySlip[0].motercycleLoan;
                    ViewBag.houseBuildingLoan = PaySlip[0].houseBuildingLoan;
                    ViewBag.landLoan = PaySlip[0].landLoan;
                    ViewBag.otherLoan = PaySlip[0].otherLoan;
                    ViewBag.tourAdvance = PaySlip[0].tourAdvance;
                    ViewBag.gisAdvance = PaySlip[0].gisAdvance;
                    ViewBag.grossEarrning = PaySlip[0].grossEarrning;
                    ViewBag.grossDeduction = PaySlip[0].grossDeduction;
                    ViewBag.netPay = PaySlip[0].netPay;
                }
                return View();
            }

            catch (Exception ex)
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult View_ApproveRejectLeave()

        {
            try
            {

                //string id = Session["usercode"].ToString();
                int id = Convert.ToInt32(Session["usercode"]);
                var data = applicationRepository.LeaveDetailsssssss().Where(x => (x.Approval1 == id && x.Status1 == true) || (x.Approval2 == id && x.Status2 == true) || (x.Approval3 == id && x.Status3 == true) || (x.Approval4 == id && x.Status4 == true) || (x.Approval5 == id && x.Status5 == true)).ToList();
                ViewBag.LeaveDetails = data;
                return View();



            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public ActionResult EPF()
        {
            return View();
        }

        public ActionResult IncomeTax()
        {
            return View();
        }

        public ActionResult Form16()
        {
            return View();
        }
    }
}