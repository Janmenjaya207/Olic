using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using OLIC.Models;
using System.Web.Mvc;
//using OLIC.Helper;
//using OLICcustom;
using BEL;
using BLL;

namespace DAL
{
    public class ConcreteLayer : AbstractLayer
    {
        OLICEntities db = new OLICEntities();
        public ManageUser CheckLogin(string username, string password)
        {
            var data = db.ManageUsers.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
            return data;
        }
        public ManageUser Mst_Ho(int id)
        {
            var data = db.ManageUsers.Where(x => x.User_Id == id).FirstOrDefault();
            return data;
        }
        public List<OLIC_Test> oLIC_Tests()
        {
            var data = db.OLIC_Test.ToList();
            return data;
        }

        public string vinno()
        {
            var data = db.DeepBorewell_Registration.ToList().Count;
            string vinno = "";
            int year = DateTime.Now.Year;

            if (data == 0)
            {
                int id = 1;
                vinno = "KHU/" + "DB/" + year + "/" + id;
            }
            else
            {
                int id = data + 1;
                vinno = "KHU/" + "DB/" + year + "/" + id;
            }
            return vinno;
        }

        public int SaveDeepBorewellApplication(FarmerRegistration FarmerRegd)
        {
            try
            {
                DeepBorewell_Registration obj = new DeepBorewell_Registration();
                obj.DbRegistrationNo = vinno();
                obj.BPLCardFile = FarmerRegd.DB_Registration.BPLCardFile;
                obj.AadhaarFile = FarmerRegd.DB_Registration.AadhaarFile;
                obj.FarmerPhoto = FarmerRegd.DB_Registration.FarmerPhoto;
                obj.AffidavitFile = FarmerRegd.DB_Registration.AffidavitFile;

                obj.FarmerName = FarmerRegd.DB_Registration.FarmerName;
                obj.MobileNo = FarmerRegd.DB_Registration.MobileNo;
                obj.Email = FarmerRegd.DB_Registration.Email;
                obj.Gender = FarmerRegd.DB_Registration.Gender;
                obj.Category = FarmerRegd.DB_Registration.Category;
                obj.AadhaarNo = FarmerRegd.DB_Registration.AadhaarNo;
                obj.BPLCardNo = FarmerRegd.DB_Registration.BPLCardNo;

                obj.Ad_Village = FarmerRegd.DB_Registration.Ad_Village;
                obj.Ad_DistrictId = FarmerRegd.DB_Registration.Ad_DistrictId;
                obj.Ad_BlockId = FarmerRegd.DB_Registration.Ad_BlockId;
                obj.Ad_GramPanchayatId = FarmerRegd.DB_Registration.Ad_GramPanchayatId;
                obj.Ad_PostOffice = FarmerRegd.DB_Registration.Ad_PostOffice;
                obj.Ad_PinCode = FarmerRegd.DB_Registration.Ad_PinCode;

                obj.Pro_Village = FarmerRegd.DB_Registration.Pro_Village;
                obj.Pro_DistrictId = FarmerRegd.DB_Registration.Pro_DistrictId;
                obj.Pro_BlockId = FarmerRegd.DB_Registration.Pro_BlockId;
                obj.Pro_GramPanchayatId = FarmerRegd.DB_Registration.Pro_GramPanchayatId;
                obj.Pro_PostOffice = FarmerRegd.DB_Registration.Pro_PostOffice;
                obj.Pro_PinCode = FarmerRegd.DB_Registration.Pro_PinCode;
                obj.IsActive = true;
                obj.IsDelete = false;
                obj.CreatedOn = DateTime.Now;
                obj.Is_Bank = false;


                db.DeepBorewell_Registration.Add(obj);
                db.SaveChanges();

                if (FarmerRegd.listdeeppatta != null)
                {
                    foreach (var item in FarmerRegd.listdeeppatta)
                    {
                        DeepBorewell_PattaDetails r = new DeepBorewell_PattaDetails();
                        r.LandPattaFile = item.Patta;
                        r.Pro_KhataNo = item.Khata;
                        r.Pro_LandArea = item.Land;
                        r.Pro_PlotNo = item.Plot;
                        r.DeepBorewellRegdId = obj.DeepBorewellRegdId;
                        r.IsActive = true;
                        r.IsDelete = false;
                        r.CreatedOn = DateTime.Now;

                        db.DeepBorewell_PattaDetails.Add(r);
                        db.SaveChanges();

                    }

                }
                return 1;
            }

            catch (Exception ex)
            {
                throw;
            }

        }

        public List<SelectListItem> District()
        {
            var data = db.OLIC_District_Mst.Where(x => x.IsActive == true).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.DistrictName,
                    Value = item.DistrictId.ToString(),
                });
            }
            return items;
        }

        public List<SelectListItem> Block()
        {
            var data = db.OLIC_Block_Ulb.Where(x => x.Is_Active == true && x.Is_Deleted == false).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.Block_Ulb_Name,
                    Value = item.Block_Ulb_Id.ToString(),
                });
            }
            return items;
        }

        public List<SelectListItem> GramPanchayat()
        {
            var data = db.OLIC_Gram_Panchayat.Where(x => x.Is_Active == true && x.Is_Deleted == false).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.GRAM_PANCHAYAT_Name,
                    Value = item.GarmPanchayat_Id.ToString(),
                });
            }
            return items;
        }
        public List<SelectListItem> Getsection()
        {
            var data = db.Mst_Section.Where(x => x.IsActive == true).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.SectionName,
                    Value = item.SectionId.ToString(),
                });
            }
            return items;
        }

        public List<SelectListItem> GetDivision()
        {
            var data = db.Mst_Division.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var item in data)
            {

                items.Add(new SelectListItem
                {
                    Value = item.DivisionId.ToString(),
                    Text = item.DivisionName,
                });
            }
            return items;
        }
        public List<SelectListItem> GetSubDivision()
        {
            var data = db.Mst_SubDivision.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var item in data)
            {
                Items.Add(new SelectListItem
                {
                    Text = item.SubDivisionName,
                    Value = item.SubDivisionId.ToString(),
                });
            }
            return Items;
        }
        public List<SelectListItem> GetCircle()
        {

            var data = db.Mst_Circle.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var item in data)
            {
                Items.Add(new SelectListItem
                {
                    Text = item.CircleName,
                    Value = item.CircleId.ToString(),
                });
            }
            return Items;
        }
        public List<SelectListItem> GetUserRole()
        {
            var data = db.UserTypes.Where(x => x.Project_Typeid == 1 && x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var item in data)
            {
                Items.Add(new SelectListItem
                {
                    Text = item.User_Name,
                    Value = item.UserType_Id.ToString(),
                });
            }
            return Items;
        }
        public bool DeleteMangeuser(int id)
        {
            var data = db.ManageUsers.Where(x => x.User_Id == id).FirstOrDefault();
            data.IsActive = false;
            data.IsDelete = true;
            db.SaveChanges();
            return true;
        }

        public int savemanageuser(ManageUsermodel model)
        {
            if (model.mangeuser.User_Id != 0)
            {
                var obj1 = db.ManageUsers.Where(x => x.User_Id == model.mangeuser.User_Id).FirstOrDefault();
                //obj1.BlockID = model.mangeuser.BlockID;
                //obj1.DistrictID = model.mangeuser.DistrictID;
                obj1.UserType_Id = model.mangeuser.UserType_Id;
                obj1.Project_Typeid = 1;
                //obj1.CircleID = model.mangeuser.CircleID;
                //obj1.DivisionID = model.mangeuser.DivisionID;
                //obj1.SubdivisionID = model.mangeuser.SubdivisionID;
                //obj1.SectionID = model.mangeuser.SectionID;
                //obj1.PanchayatID = model.mangeuser.PanchayatID;
                obj1.FirstName = model.mangeuser.FirstName;
                obj1.LastName = model.mangeuser.LastName;
                obj1.Email = model.mangeuser.Email;
                obj1.MobileNo = model.mangeuser.MobileNo;
                obj1.UserName = model.mangeuser.UserName;
                obj1.Password = model.mangeuser.Password;
                obj1.ModBy = model.mangeuser.CreatedBy;
                obj1.ModOn = DateTime.Now;
                obj1.IsActive = true;
                obj1.IsDelete = false;
                db.SaveChanges();
                return 1;
            }
            else
            {
                //Area a = new Area();
                ManageUser obj1 = new ManageUser();
                //obj1.Areaname = model.AreaName;
                //obj1.BlockID = model.mangeuser.BlockID;
                //obj1.DistrictID = model.mangeuser.DistrictID;
                obj1.UserType_Id = model.mangeuser.UserType_Id;
                obj1.Project_Typeid = 1;
                //obj1.CircleID = model.mangeuser.CircleID;
                //obj1.DivisionID = model.mangeuser.DivisionID;
                //obj1.SubdivisionID = model.mangeuser.SubdivisionID;
                //obj1.SectionID = model.mangeuser.SectionID;
                //obj1.PanchayatID = model.mangeuser.PanchayatID;
                obj1.FirstName = model.mangeuser.FirstName;
                obj1.LastName = model.mangeuser.LastName;
                obj1.Email = model.mangeuser.Email;
                obj1.MobileNo = model.mangeuser.MobileNo;
                obj1.UserName = model.mangeuser.UserName;
                obj1.Password = model.mangeuser.Password;
                obj1.CreatedBy = model.mangeuser.CreatedBy;
                obj1.CreatedOn = DateTime.Now;
                obj1.IsActive = true;
                obj1.IsDelete = false;
                db.ManageUsers.Add(obj1);
                db.SaveChanges();
                return 2;
            }

        }


        public int Deleteapplyleave(int idd)
        {


            Hcm_ApplyLeave hcm_ApplyLeave = db.Hcm_ApplyLeave.Where(x => x.EmployeeID == idd).FirstOrDefault();
            db.Hcm_ApplyLeave.Remove(hcm_ApplyLeave);
            db.SaveChanges();
            return 1;

        }
        public List<SelectListItem> EEBlock()
        {
            var data = db.OLIC_Block_Ulb.Where(x => x.Is_Active == true && x.Is_Deleted == false && x.District_Id == 1).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.Block_Ulb_Name,
                    Value = item.Block_Ulb_Id.ToString(),
                });
            }
            return items;
        }

        public List<SelectListItem> EEGramPanchayat()
        {
            var data = db.OLIC_Gram_Panchayat.Where(x => x.Is_Active == true && x.Is_Deleted == false).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.GRAM_PANCHAYAT_Name,
                    Value = item.GarmPanchayat_Id.ToString(),
                });
            }
            return items;
        }

        public List<Vw_DeepBorewellRegdDtl> DbAppDetails()
        {
            var data = db.Vw_DeepBorewellRegdDtl.ToList();
            return data;
        }

        public List<sp_DeepBorewell_PattaDetails_Result> sp_DeepBorewell_PattaDetails_Result(int id, string url)
        {
            var data = db.sp_DeepBorewell_PattaDetails(id, url).ToList();
            return data;
        }

        public List<sp_deepBorewell_REgdeatils_Result> sp_deepBorewell_REgdeatils_Result(int id)
        {
            var data = db.sp_deepBorewell_REgdeatils(id).ToList();
            return data;
        }

        public int SavePaymentStatus(Paymentstatus Paymentstatus)
        {
            var dta = db.BankUpdatePayments.Add(Paymentstatus.BankUpdatePayment);

            db.SaveChanges();
            var data = db.DeepBorewell_Registration.Where(x => x.DeepBorewellRegdId == dta.DeepBorewellRegdId).FirstOrDefault();
            data.Is_Bank = true;
            db.SaveChanges();
            return 1;

        }

        public int Saveverify_application(ApprovalprocessEE bg)
        {
            int count = 0;
            if (bg.ListAssign != null)
            {
                foreach (var item1 in bg.ListAssign)
                {
                    var ff = db.DeepBorewell_Registration.Where(x => x.DeepBorewellRegdId == item1.Applicationidd).FirstOrDefault();
                    ff.Is_EE = true;

                    db.SaveChanges();

                }

                EE_Verify nw = new EE_Verify();
                {
                    nw.District = bg.EE_Verify.District;
                    nw.block = bg.EE_Verify.block;
                    nw.panchayat = bg.EE_Verify.panchayat;
                    nw.Remarks = bg.EE_Verify.Remarks;
                    nw.AssignTo = bg.EE_Verify.AssignTo;
                    nw.Addon = DateTime.Now;
                    nw.Is_Active = true;
                    nw.Is_Delete = false;
                    db.EE_Verify.Add(nw);
                    db.SaveChanges();
                }
                count = 1;
            }
            return count;
        }

        public int SaveDBWMstDPR(MasterModel DprMasterModel)
        {
            try
            {
                int count = 0;

                if (DprMasterModel.DBW_Mst_DPR.Dpr_Id != 0 && DprMasterModel.DBW_Mst_DPR.Dpr_Id != null)
                {
                    var obj = db.DBW_Mst_DPR.Where(x => x.Dpr_Id == DprMasterModel.DBW_Mst_DPR.Dpr_Id).FirstOrDefault();
                    obj.Description_of_work = DprMasterModel.DBW_Mst_DPR.Description_of_work;
                    obj.Unit = DprMasterModel.DBW_Mst_DPR.Unit;
                    obj.Rate_per_Unit = DprMasterModel.DBW_Mst_DPR.Rate_per_Unit;
                    obj.Remarks = DprMasterModel.DBW_Mst_DPR.Remarks;
                    obj.IsActive = true;
                    obj.IsDeleted = false;
                    db.SaveChanges();
                    count = 2;
                }
                else
                {
                    DBW_Mst_DPR obj = new DBW_Mst_DPR();
                    obj.Description_of_work = DprMasterModel.DBW_Mst_DPR.Description_of_work;
                    obj.Unit = DprMasterModel.DBW_Mst_DPR.Unit;
                    obj.Rate_per_Unit = DprMasterModel.DBW_Mst_DPR.Rate_per_Unit;
                    obj.Remarks = DprMasterModel.DBW_Mst_DPR.Remarks;
                    obj.IsActive = true;
                    obj.IsDeleted = false;
                    db.DBW_Mst_DPR.Add(obj);
                    db.SaveChanges();
                    count = 1;
                }
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<DBW_Mst_DPR> dBW_Mst_DPRs()
        {
            var data = db.DBW_Mst_DPR.Where(x => x.IsActive == true).ToList();
            return data;
        }

        public int SaveOlicMaster(List<OlicMstDtl_SAP> olicMstDtl_SAP)
        {
            try
            {
                int count = 0;
                foreach (var item in olicMstDtl_SAP)
                {
                    var data = db.Mst_OlicDtl.Where(x => x.SectionId == item.section).FirstOrDefault();
                    if (data != null)
                    {
                        data.CircleId = item.circle;
                        data.CircleName = item.circleDesc;
                        data.DivisionId = item.division;
                        data.DivisionName = item.divisionDesc;
                        data.SubDivisionId = item.subDivision;
                        data.SubDivisionName = item.subDivisionDesc;
                        data.SectionId = item.section;
                        data.SectionName = item.sectionDesc;
                        data.IsActive = true;
                        data.IsDeleted = false;
                        db.SaveChanges();
                        count++;
                    }
                    else
                    {
                        Mst_OlicDtl obj = new Mst_OlicDtl();
                        obj.CircleId = item.circle;
                        obj.CircleName = item.circleDesc;
                        obj.DivisionId = item.division;
                        obj.DivisionName = item.divisionDesc;
                        obj.SubDivisionId = item.subDivision;
                        obj.SubDivisionName = item.subDivisionDesc;
                        obj.SectionId = item.section;
                        obj.SectionName = item.sectionDesc;
                        obj.IsActive = true;
                        obj.IsDeleted = false;
                        db.Mst_OlicDtl.Add(obj);
                        db.SaveChanges();
                        count++;
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int SaveCircleMaster(List<CircleMaster_SAP> circleMaster_SAPs)
        {
            try
            {
                int count = 0;
                foreach (var item in circleMaster_SAPs)
                {
                    var data = db.Mst_Circle.Where(x => x.CircleId == item.circle).FirstOrDefault();
                    if (data != null)
                    {
                        data.CircleId = item.circle;
                        data.CircleName = item.circleDesc;
                        data.IsActive = true;
                        data.IsDelete = false;
                        db.SaveChanges();
                        count++;
                    }
                    else
                    {
                        Mst_Circle obj = new Mst_Circle();
                        obj.CircleId = item.circle;
                        obj.CircleName = item.circleDesc;
                        obj.IsActive = true;
                        obj.IsDelete = false;
                        db.Mst_Circle.Add(obj);
                        db.SaveChanges();
                        count++;
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Mst_Circle> mst_Circles()
        {
            var data = db.Mst_Circle.Where(x => x.IsActive == true).ToList();
            return data;
        }

        public int SaveDivisionMaster(IList<DivisionMaster_SAP> divisionMaster_SAPs)
        {
            try
            {
                int count = 0;
                foreach (var item in divisionMaster_SAPs)
                {
                    var data = db.Mst_Division.Where(x => x.DivisionId == item.division).FirstOrDefault();
                    if (data != null)
                    {
                        data.CircleId = item.circle;
                        data.DivisionId = item.division;
                        data.DivisionName = item.divisionDesc;
                        data.IsActive = true;
                        data.IsDelete = false;
                        db.SaveChanges();
                        count++;
                    }
                    else
                    {
                        Mst_Division obj = new Mst_Division();
                        obj.CircleId = item.circle;
                        obj.DivisionId = item.division;
                        obj.DivisionName = item.divisionDesc;
                        obj.IsActive = true;
                        obj.IsDelete = false;
                        db.Mst_Division.Add(obj);
                        db.SaveChanges();
                        count++;
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Mst_Division> mst_Divisions()
        {
            var data = db.Mst_Division.Where(x => x.IsActive == true).ToList();
            return data;
        }

        public int SaveSubDivisionMaster(IList<SubDivisionMaster_SAP> subDivisionMaster_SAPs)
        {
            try
            {
                int count = 0;
                foreach (var item in subDivisionMaster_SAPs)
                {
                    var data = db.Mst_SubDivision.Where(x => x.SubDivisionId == item.subDivision).FirstOrDefault();
                    if (data != null)
                    {
                        data.SubDivisionId = item.subDivision;
                        data.DivisionId = item.division;
                        data.SubDivisionName = item.subDivisionDesc;
                        data.IsActive = true;
                        data.IsDelete = false;
                        db.SaveChanges();
                        count++;
                    }
                    else
                    {
                        Mst_SubDivision obj = new Mst_SubDivision();
                        obj.SubDivisionId = item.subDivision;
                        obj.DivisionId = item.division;
                        obj.SubDivisionName = item.subDivisionDesc;
                        obj.IsActive = true;
                        obj.IsDelete = false;
                        db.Mst_SubDivision.Add(obj);
                        db.SaveChanges();
                        count++;
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Mst_SubDivision> mst_SubDivisions()
        {
            var data = db.Mst_SubDivision.Where(x => x.IsActive == true).ToList();
            return data;
        }

        public int SaveSectionMaster(IList<SectionMaster_SAP> sectionMaster_SAPs)
        {
            try
            {
                int count = 0;
                foreach (var item in sectionMaster_SAPs)
                {
                    var data = db.Mst_Section.Where(x => x.SectionId == item.section).FirstOrDefault();
                    if (data != null)
                    {
                        data.SubDivisionId = item.subDivision;
                        data.SectionId = item.section;
                        data.SectionName = item.sectionDesc;
                        data.IsActive = true;
                        data.IsDelete = false;
                        db.SaveChanges();
                        count++;
                    }
                    else
                    {
                        Mst_Section obj = new Mst_Section();
                        obj.SubDivisionId = item.subDivision;
                        obj.SectionId = item.section;
                        obj.SectionName = item.sectionDesc;
                        obj.IsActive = true;
                        obj.IsDelete = false;
                        db.Mst_Section.Add(obj);
                        db.SaveChanges();
                        count++;
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Mst_Section> mst_Sections()
        {
            var data = db.Mst_Section.Where(x => x.IsActive == true).ToList();
            return data;
        }

        #region--------------------------------------------Scrap---------------------------------------
        public int Savescrap_Category(List<ScrapSAPMasterModel> olicMstDtl_SAP)
        {
            try
            {
                int count = 0;
                foreach (var item in olicMstDtl_SAP)
                {
                    var data = db.Scrap_Catrgory_Mst.Where(x => x.MaterialType == item.materialType).FirstOrDefault();
                    if (data != null)
                    {
                        data.MaterialType = item.materialType;
                        data.Category_Name = item.materialTypeText;

                        data.IsActive = true;
                        data.IsDelete = false;
                        db.SaveChanges();
                        count++;

                    }
                    else
                    {
                        Scrap_Catrgory_Mst obj = new Scrap_Catrgory_Mst();
                        obj.MaterialType = item.materialType;
                        obj.Category_Name = item.materialTypeText;

                        obj.IsActive = true;
                        obj.IsDelete = false;
                        db.Scrap_Catrgory_Mst.Add(obj);
                        db.SaveChanges();
                        count++;
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Scrap_Item_Mst(List<Scrap_Iteam_master_Model> Scrap_Iteam_master_Model)
        {
            try
            {
                int count = 0;
                foreach (var item in Scrap_Iteam_master_Model)
                {
                    var data = db.Scrap_Item_Mst.Where(x => x.Matreial_Code == item.matreial).FirstOrDefault();
                    if (data != null)
                    {
                        data.MaterialType = item.materialType;
                        data.Matreial_Code = item.matreial;
                        data.Iteam_name = item.materialText;
                        data.IsActive = true;
                        data.IsDelete = false;
                        db.SaveChanges();
                        count++;

                    }
                    else
                    {
                        Scrap_Item_Mst obj = new Scrap_Item_Mst();
                        obj.MaterialType = item.materialType;
                        obj.Matreial_Code = item.matreial;
                        obj.Iteam_name = item.materialText;
                        obj.IsActive = true;
                        obj.IsDelete = false;
                        db.Scrap_Item_Mst.Add(obj);
                        db.SaveChanges();
                        count++;
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Scrap_Details_Save(Scrap_Details_Entry_MainModel sdetail)
        {
            Scrap_code s = new Scrap_code();
            s.Scrap_Code_Name = Scrap_Code();
            s.Remark = sdetail.Remark;
            s.IsActive = true;
            s.IsDelete = false;
            db.Scrap_code.Add(s);
            db.SaveChanges();
            foreach (var item in sdetail.Scrap_Details)
            {
                Scrap_Details_Entry scrap_Details_Entry = new Scrap_Details_Entry();
                scrap_Details_Entry.Scrap_Code_Autogenerate_Id = s.Scrap_Code_Autogenerate_Id;
                scrap_Details_Entry.Scrap_Category_Id = item.Scrap_Category_Id;
                scrap_Details_Entry.Scrap_Iteam_Id = item.Scrap_Iteam_Id;
                scrap_Details_Entry.Rate = item.rate;
                scrap_Details_Entry.Amount = item.amount;
                scrap_Details_Entry.Approx_Weight = item.aw;
                scrap_Details_Entry.Total_Weight = item.tw;
                scrap_Details_Entry.Date_Of_Receipt = item.dor;
                scrap_Details_Entry.No_Of_Scrap = item.no_of_scrap;
                scrap_Details_Entry.Amount = item.amount;

                scrap_Details_Entry.Is_Active = true;
                scrap_Details_Entry.Is_Delete = false;

                db.Scrap_Details_Entry.Add(scrap_Details_Entry);

                db.SaveChanges();

            }
            return 1;
        }

        public string Scrap_Code()
        {
            var data = db.Scrap_code.ToList().Count;

            int year = DateTime.Now.Year;
            string data2 = "";
            if (data == 0)
            {
                int id = 1;


                data2 = "Scrap-" + year + "-" + id;
            }
            else
            {
                int id = data + 1;
                data2 = "Scrap-" + year + "-" + id;
            }
            return data2;
        }








        #endregion-------------------------------------Scrap-------------------------------------------



        #region-----------------------------------Hcm-----------------------------------------------------

        public List<HCM_EmployeeDtl> hcm_EmployeeDtls()
        {
            var data = db.HCM_EmployeeDtl.Where(x => x.IsActive == true).ToList();
            return data;
        }

        public List<Hcm_Property_Land> hcm_LandDtls()
        {
            var data = db.Hcm_Property_Land.ToList();
            return data;

        }

        public List<Hcm_Property_House> hcm_HouseDtls()
        {
            var data = db.Hcm_Property_House.ToList();
            return data;
        }

        public List<Hcm_Property_Land> hcm_LandDtls(string uid)
        {
            var data = (from e in db.Hcm_Property_Land
                        where e.EmpCode == uid
                        select e).ToList();

            return data;

        }

        public List<Hcm_Property_House> hcm_HouseDtls(string uid)
        {
            var data = (from e in db.Hcm_Property_House
                        where e.EmpCode == uid
                        select e).ToList();

            return data;
        }

        public List<vwEmployeesByLeaveType> hcm_LeaveDetails(int id)
        {
            var data = db.vwEmployeesByLeaveTypes.Where(x => x.Is_Active == true && x.EmployeeID == id).ToList();

            return data;
        }
        public List<vwEmployeesByLeaveType> hcm_LeaveDetails(string id)
        {
            var data = db.vwEmployeesByLeaveTypes.Where(x => x.Is_Active == true && x.EmployeeCode == id).ToList();
            return data;
        }

        public List<vwEmployeesByLeaveType> LeaveDetailss(int idd)
        {
            var data = db.vwEmployeesByLeaveTypes.Where(x => x.Is_Active == true && x.PendingAt == idd.ToString()).ToList();

            return data;
        }

        public List<vwEmployeesByLeaveType> LeaveDetailss(string idd)
        {
            var data = db.vwEmployeesByLeaveTypes.Where(x => x.Is_Active == true && x.PendingAt == idd.ToString()).ToList();

            //var data = db.vwEmployeesByLeaveTypes.Where(x => x.Is_Active == true && x.EmployeeID == Id).ToList();

            return data;

        }

        public List<vwEmployeesByLeaveType> LeaveDetailsssssss()
        {
            var data = db.vwEmployeesByLeaveTypes.Where(x => x.Is_Active == true).ToList();

            return data;
        }

        public List<Hcm_Property_ImmovableProperties> hcm_ImmovablepropertyDtls()
        {
            var data = db.Hcm_Property_ImmovableProperties.ToList();
            return data;
        }

        public List<Hcm_Property_MovableProperty> hcm_MovablepropertyDtls()
        {
            var data = db.Hcm_Property_MovableProperty.ToList();
            return data;
        }

        public List<Hcm_Property_OtherMovable> hcm_OMovablepropertyDtls()
        {
            var data = db.Hcm_Property_OtherMovable.ToList();
            return data;
        }

        public List<Hcm_Property_ImmovableProperties> hcm_ImmovablepropertyDtls(string uid)
        {
            var data = (from e in db.Hcm_Property_ImmovableProperties
                        where e.EmpCode == uid
                        select e).ToList();

            return data;
        }

        public List<Hcm_Property_MovableProperty> hcm_MovablepropertyDtls(string uid)
        {
            var data = (from e in db.Hcm_Property_MovableProperty
                        where e.EmpCode == uid
                        select e).ToList();
            return data;
        }

        public List<Hcm_Property_OtherMovable> hcm_OMovablepropertyDtls(string uid)
        {
            var data = (from e in db.Hcm_Property_OtherMovable
                        where e.EmpCode == uid
                        select e).ToList();
            return data;
        }

        public List<Hcm_Property> hcm_PropertyDtls()
        {
            var data = db.Hcm_Property.ToList();
            return data;
        }

        public List<HCM_LeaveBalance> hcm_LeaveMasterDtls(string uname)
        {
            var data = (from e in db.HCM_LeaveBalance
                        where e.Employee_Code == uname
                        select e).ToList();


            //var data = (from e in db.HCM_LeaveBalance
            //            join f in db.HCM_LeaveMaster on e.LeaveType equals f.LeaveType
            //            where e.Employee_Code == uname
            //            select new { e.LeaveAllocated,e.LeaveAvailable,f.LeaveType}).ToList();


            return data;
        }

        public int SaveHcmEmp(EmployeePropertyDetails employeePropertyDetails)
        {
            Hcm_Property_Land obj = new Hcm_Property_Land();

            if (employeePropertyDetails.ListOfLands.Count > 0)
            {
                foreach (var item in employeePropertyDetails.ListOfLands)
                {
                    Hcm_Property_Land r = new Hcm_Property_Land();
                    r.PreciseLocation = item.PreciseLocation;
                    r.InWhoseName = item.InWhoseName;
                    r.Value = item.Value;
                    r.ExtentOfInterest = item.ExtentOfInterest;
                    r.DateAndMannerOfAcquisitionOrDisposal = Convert.ToDateTime(item.DateAndMannerOfAcquisitionOrDisposal);
                    r.Files = item.Files;
                    r.EmpCode = item.EmpCode;
                    db.Hcm_Property_Land.Add(r);
                    db.SaveChanges();

                }
            }

            if (employeePropertyDetails.ListOfHouses.Count > 0)
            {
                foreach (var item in employeePropertyDetails.ListOfHouses)
                {
                    Hcm_Property_House r = new Hcm_Property_House();
                    r.PreciseLocation = item.PreciseLocation;
                    r.InWhoseName = item.InWhoseName;
                    r.Value = item.Value;
                    r.ExtentLOfInterest = item.ExtentLOfInterest;
                    r.DateAndManner = (DateTime)item.DateAndManner;
                    r.Files = item.Files;
                    r.EmpCode = item.EmpCode;
                    db.Hcm_Property_House.Add(r);
                    db.SaveChanges();

                }
            }

            if (employeePropertyDetails.ListOfImmovableproperties.Count > 0)
            {
                foreach (var item in employeePropertyDetails.ListOfImmovableproperties)
                {
                    Hcm_Property_ImmovableProperties r = new Hcm_Property_ImmovableProperties();
                    r.BreifDescription = item.BreifDescription;
                    r.ExtentOfInterest = item.ExtentOfInterest;
                    r.Value = item.Value;
                    r.InWhoseName = item.InWhoseName;
                    r.DateAndManner = (DateTime)item.DateAndMannerOfAcquisitionOrDiposal;
                    r.Files = item.Files;
                    r.Remarks = item.Remarks;
                    r.EmpCode = item.EmpCode;
                    db.Hcm_Property_ImmovableProperties.Add(r);
                    db.SaveChanges();
                }
            }

            if (employeePropertyDetails.ListOfMovableproperties.Count > 0)
            {
                foreach (var item in employeePropertyDetails.ListOfMovableproperties)
                {
                    Hcm_Property_MovableProperty r = new Hcm_Property_MovableProperty();
                    r.DescriptionOfItem = item.DescriptionOfItems;
                    r.DateAndManner = (DateTime)item.DateAndManner;
                    r.InWhoseName = item.InWhoseName;
                    r.Loans = item.Loans;
                    r.Remarks = item.Remarks;
                    r.Value = item.Value;
                    r.Files = item.Files;
                    r.EmpCode = item.EmpCode;
                    db.Hcm_Property_MovableProperty.Add(r);
                    db.SaveChanges();

                }
            }

            if (employeePropertyDetails.ListOfOthermovables.Count > 0)
            {
                foreach (var item in employeePropertyDetails.ListOfOthermovables)
                {
                    Hcm_Property_OtherMovable r = new Hcm_Property_OtherMovable();

                    r.DescriptionOfItems = item.DescriptionOfItems;
                    r.InWhoseName = item.InWhoseName;
                    r.Value = item.Value;
                    r.Remarks = item.Remarks;
                    r.DateAndManner = item.DateAndManner;
                    r.Files = item.Files;
                    r.EmpCode = item.EmpCode;
                    db.Hcm_Property_OtherMovable.Add(r);
                    db.SaveChanges();

                }
            }

            return 1;
        }

        public int EditHcmEmp(EmployeePropertyDetails employeePropertyDetails)
        {

            if (employeePropertyDetails.ListOfLands.Count > 0)
            {
                foreach (var item in employeePropertyDetails.ListOfLands)
                {
                    Hcm_Property_Land r = new Hcm_Property_Land();

                    var rr = db.Hcm_Property_Land.Where(s => s.Id == item.Id).FirstOrDefault();

                    db.Hcm_Property_Land.Remove(rr);
                    db.SaveChanges();
                    r.PreciseLocation = item.PreciseLocation;
                    r.InWhoseName = item.InWhoseName;
                    r.Value = item.Value;
                    r.ExtentOfInterest = item.ExtentOfInterest;
                    r.DateAndMannerOfAcquisitionOrDisposal = Convert.ToDateTime(item.DateAndMannerOfAcquisitionOrDisposal);
                    r.Files = item.Files;
                    r.EmpCode = item.EmpCode;
                    db.Hcm_Property_Land.Add(r);
                    db.SaveChanges();

                }
            }

            if (employeePropertyDetails.ListOfHouses.Count > 0)
            {
                foreach (var item in employeePropertyDetails.ListOfHouses)
                {
                    Hcm_Property_House r = new Hcm_Property_House();

                    var rr = db.Hcm_Property_House.Where(H => H.Id == item.Id).FirstOrDefault();

                    db.Hcm_Property_House.Remove(rr);
                    db.SaveChanges();
                    r.PreciseLocation = item.PreciseLocation;
                    r.InWhoseName = item.InWhoseName;
                    r.Value = item.Value;
                    r.ExtentLOfInterest = item.ExtentLOfInterest;
                    r.DateAndManner = item.DateAndManner;
                    r.Files = item.Files;
                    r.EmpCode = item.EmpCode;
                    db.Hcm_Property_House.Add(r);
                    db.SaveChanges();

                }
            }


            if (employeePropertyDetails.ListOfImmovableproperties.Count > 0)
            {
                foreach (var item in employeePropertyDetails.ListOfImmovableproperties)
                {
                    Hcm_Property_ImmovableProperties r = new Hcm_Property_ImmovableProperties();

                    var rr = db.Hcm_Property_ImmovableProperties.Where(s => s.Id == item.Id).FirstOrDefault();
                    if (rr != null)
                    {
                        db.Hcm_Property_ImmovableProperties.Remove(rr);
                    }

                    db.SaveChanges();
                    r.BreifDescription = item.BreifDescription;
                    r.InWhoseName = item.InWhoseName;
                    r.Value = item.Value;
                    r.ExtentOfInterest = item.ExtentOfInterest;
                    r.DateAndManner = item.DateAndMannerOfAcquisitionOrDiposal;
                    r.Files = item.Files;
                    r.EmpCode = item.EmpCode;
                    db.Hcm_Property_ImmovableProperties.Add(r);
                    db.SaveChanges();

                }
            }

            if (employeePropertyDetails.ListOfMovableproperties.Count > 0)
            {
                foreach (var item in employeePropertyDetails.ListOfMovableproperties)
                {
                    Hcm_Property_MovableProperty r = new Hcm_Property_MovableProperty();

                    var rr = db.Hcm_Property_MovableProperty.Where(s => s.Id == item.Id).FirstOrDefault();

                    if (rr != null)
                    {
                        db.Hcm_Property_MovableProperty.Remove(rr);
                    }

                    db.SaveChanges();
                    r.DescriptionOfItem = item.DescriptionOfItems;
                    r.InWhoseName = item.InWhoseName;
                    r.Value = item.Value;
                    r.DateAndManner = item.DateAndManner;
                    r.Remarks = item.Remarks;
                    r.Files = item.Files;
                    r.EmpCode = item.EmpCode;
                    db.Hcm_Property_MovableProperty.Add(r);
                    db.SaveChanges();
                }
            }

            if (employeePropertyDetails.ListOfOthermovables.Count > 0)
            {
                foreach (var item in employeePropertyDetails.ListOfOthermovables)
                {
                    Hcm_Property_OtherMovable r = new Hcm_Property_OtherMovable();

                    var rr = db.Hcm_Property_OtherMovable.Where(s => s.Id == item.Id).FirstOrDefault();

                    if (rr != null)
                    {
                        db.Hcm_Property_OtherMovable.Remove(rr);
                    }

                    db.SaveChanges();
                    r.DescriptionOfItems = item.DescriptionOfItems;
                    r.InWhoseName = item.InWhoseName;
                    r.Value = item.Value;
                    r.DateAndManner = item.DateAndManner;
                    r.Remarks = item.Remarks;
                    r.Files = item.Files;
                    r.EmpCode = item.EmpCode;
                    db.Hcm_Property_OtherMovable.Add(r);
                    db.SaveChanges();

                }
            }

            return 1;
        }

        public List<SelectListItem> Leavetypelist()
        {
            var data = db.HCM_LeaveMaster.Where(x => x.IsActive == true && x.IsDeleted == false).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.LeaveType,
                    Value = item.LeaveCode.ToString(),
                });
            }
            return items;
        }

        public int saveapplyleave(ApplyLeaveModel leave)
        {
            var data1 = db.Hcm_ApplyLeave.Where(x => x.FromDate <= leave.applyLeavelr.FromDate && x.ToDate >= leave.applyLeavelr.ToDate && x.EmployeeCode == leave.applyLeavelr.EmployeeCode && x.Leave_Typeid == leave.applyLeavelr.Leave_Typeid).FirstOrDefault();
            var data = db.HCM_EmployeeDtl.Where(x => x.Employee_Code == leave.applyLeavelr.EmployeeCode && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (data != null && data1 == null)

            {
                Hcm_ApplyLeave obj = new Hcm_ApplyLeave();
                obj.EmployeeCode = leave.applyLeavelr.EmployeeCode;
                obj.EmployeeID = leave.applyLeavelr.EmployeeID;
                obj.Leave_Typeid = leave.applyLeavelr.Leave_Typeid;
                obj.FromDate = leave.applyLeavelr.FromDate;
                obj.ToDate = leave.applyLeavelr.ToDate;
                obj.Count = Convert.ToInt32(leave.applyLeavelr.Count);
                obj.Remarks = leave.applyLeavelr.Remarks;
                obj.Is_Active = true;
                obj.Is_Delete = false;
                obj.Approval1 = Convert.ToInt32(data.Approval1);
                obj.Approval2 = Convert.ToInt32(data.Approval2);
                obj.Approval3 = Convert.ToInt32(data.Approval3);
                obj.Approval4 = Convert.ToInt32(data.Approval4);
                obj.Approval5 = Convert.ToInt32(data.Approval5);
                obj.PendingAt = data.Approval1.ToString();
                obj.Approverejct = "Approval In Progress";
                obj.Status1 = false;
                obj.Status2 = false;
                obj.Status3 = false;
                obj.Status4 = false;
                obj.Status5 = false;
                obj.Addby = leave.applyLeavelr.EmployeeID;
                obj.Addon = DateTime.Now;
                db.Hcm_ApplyLeave.Add(obj);
                db.SaveChanges();
                return 1;
            }
            return 3;
        }

        public int Savehcmemployeedetails(List<HCM_EmployeeDetails> circleMaster_SAPs)
        {
            try
            {
                int count = 0;
                foreach (var item in circleMaster_SAPs)
                {
                    int id = Convert.ToInt32(item.EmployeeId);
                    var obj1 = db.HCM_EmployeeDtl.Where(x => x.Employee_Id == id).FirstOrDefault();
                    if (obj1 != null)
                    {
                        obj1.Employee_Id = Convert.ToInt32(item.EmployeeId);
                        obj1.Employee_Code = item.EmployeeId;

                        obj1.Employee_Name = item.EmployeeName;

                        obj1.Email_Id = item.EmailId;
                        obj1.Mobile_No = item.MobileNo;
                        obj1.Department = item.Department;
                        obj1.Designation = item.Desination;
                        obj1.IsActive = true;
                        obj1.IsDeleted = false;
                        obj1.Emp_Class = item.Type;
                        obj1.Gender = item.Gender;
                        obj1.DOJ = Convert.ToDateTime(item.DateOfJoin);
                        obj1.Area = item.Area;
                        obj1.Bank_Name = item.BankName;
                        obj1.Ac_No = item.AccountNo;
                        obj1.IFSC = item.IfscCode;
                        obj1.UAN = item.Uan;
                        obj1.GIS_No = item.GisNo;
                        obj1.Bill_No = item.Bill_No;
                        obj1.PF_No = item.Pf;
                        obj1.PAN = item.Pan;
                        int? Approva1 = Convert.ToInt32(item.Approval1);
                        obj1.Approval1 = Approva1;
                        int? Approva2 = Convert.ToInt32(item.Approval2);
                        obj1.Approval2 = Approva2;
                        int? Approva3 = Convert.ToInt32(item.Approval3);
                        obj1.Approval3 = Approva3;
                        int? Approva4 = Convert.ToInt32(item.Approval4);
                        obj1.Approval4 = Approva4;
                        int? Approva5 = Convert.ToInt32(item.Approval5);
                        obj1.Approval5 = Approva5;
                        db.SaveChanges();
                        count++;
                    }
                    else
                    {
                        HCM_EmployeeDtl obj = new HCM_EmployeeDtl();
                        obj.Employee_Id = Convert.ToInt32(item.EmployeeId);
                        obj.Employee_Name = item.EmployeeName;
                        obj.Employee_Code = item.EmployeeId;
                        obj.Email_Id = item.EmailId;
                        obj.Mobile_No = item.MobileNo;
                        obj.Department = item.Department;
                        obj.Designation = item.Desination;
                        obj.IsActive = true;
                        obj.IsDeleted = false;
                        obj.Emp_Class = item.Type;
                        obj.Gender = item.Gender;
                        obj.DOJ = Convert.ToDateTime(item.DateOfJoin);
                        obj1.Bank_Name = item.BankName;
                        obj1.Ac_No = item.AccountNo;
                        obj1.IFSC = item.IfscCode;
                        obj1.UAN = item.Uan;
                        obj1.GIS_No = item.GisNo;
                        obj1.Bill_No = item.Bill_No;
                        obj1.PF_No = item.Pf;
                        obj.Area = item.Area;
                        int? Approva1 = Convert.ToInt32(item.Approval1);
                        obj.Approval1 = Approva1;
                        int? Approva2 = Convert.ToInt32(item.Approval2);
                        obj.Approval2 = Approva2;
                        int? Approva3 = Convert.ToInt32(item.Approval3);
                        obj.Approval3 = Approva3;
                        int? Approva4 = Convert.ToInt32(item.Approval4);
                        obj.Approval4 = Approva4;
                        int? Approva5 = Convert.ToInt32(item.Approval5);
                        obj.Approval5 = Approva5;
                        db.HCM_EmployeeDtl.Add(obj);
                        db.SaveChanges();
                        count++;
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Resolve(string regid, int id, string remark)
        {
            var data = db.Hcm_ApplyLeave.Where(x => x.Leave_Id == id).FirstOrDefault();
            if (data.Remarks1 == null && data.Status1 == false && data.PendingAt == regid.ToString())
            {
                data.Remarks1 = remark;
                data.Status1 = true;
                if (data.Approval2 == 0)
                {
                    data.PendingAt = null;
                    data.Approverejct = "Approved";
                }
                else
                {
                    data.PendingAt = data.Approval2.ToString();
                }
                db.SaveChanges();
            }

            if (data.Remarks2 == null && data.Status2 == false && data.PendingAt == regid.ToString())
            {
                data.Remarks2 = remark;
                data.Status2 = true;
                if (data.Approval3 == 0)
                {
                    data.PendingAt = null;
                    data.Approverejct = "Approved";
                }
                else
                {
                    data.PendingAt = data.Approval3.ToString();
                }
                db.SaveChanges();
            }

            if (data.Remarks3 == null && data.Status3 == false && data.PendingAt == regid.ToString())
            {
                data.Remarks3 = remark;
                data.Status3 = true;
                if (data.Approval4 == 0)
                {
                    data.PendingAt = null;
                    data.Approverejct = "Approved";
                }
                else
                {
                    data.PendingAt = data.Approval4.ToString();
                }
                db.SaveChanges();
            }

            if (data.Remarks4 == null && data.Status4 == false && data.PendingAt == regid.ToString())
            {
                data.Remarks4 = remark;
                data.Status4 = true;
                if (data.Approval5 == 0)
                {
                    data.PendingAt = null;
                    data.Approverejct = "Approved";
                }
                else
                {
                    data.PendingAt = data.Approval5.ToString();
                }
                db.SaveChanges();
            }

            if (data.Remarks5 == null && data.Status5 == false && data.PendingAt == regid.ToString())
            {
                data.Remarks5 = remark;
                data.Status5 = true;
                data.PendingAt = null;
                data.Approverejct = "Approved";
                db.SaveChanges();
            }

            return true;
        }

        public bool Reject(string regid, int id, string remark)
        {
            var data = db.Hcm_ApplyLeave.Where(x => x.Leave_Id == id).FirstOrDefault();
            if (data.Remarks1 == null && data.Status1 == false && data.PendingAt == regid.ToString())
            {
                data.Remarks1 = remark;
                data.Status1 = true;
                data.PendingAt = null;
                data.Approverejct = "Rejected";
                db.SaveChanges();
            }

            if (data.Remarks2 == null && data.Status2 == false && data.PendingAt == regid.ToString())
            {
                data.Remarks2 = remark;
                data.Status2 = true;
                data.PendingAt = null;
                data.Approverejct = "Rejected";
                db.SaveChanges();
            }

            if (data.Remarks3 == null && data.Status3 == false && data.PendingAt == regid.ToString())
            {
                data.Remarks3 = remark;
                data.Status3 = true;
                data.PendingAt = null;
                data.Approverejct = "Rejected";
                db.SaveChanges();
            }

            if (data.Remarks4 == null && data.Status4 == false && data.PendingAt == regid.ToString())
            {
                data.Remarks4 = remark;
                data.Status4 = true;
                data.PendingAt = null;
                data.Approverejct = "Rejected";
                db.SaveChanges();
            }

            if (data.Remarks5 == null && data.Status5 == false && data.PendingAt == regid.ToString())
            {
                data.Remarks5 = remark;
                data.Status5 = true;
                data.PendingAt = null;
                data.Approverejct = "Rejected";
                db.SaveChanges();
            }

            return true;
        }



        //public List<HCM_LeaveBalance> hcm_LeaveBalanceDtls()
        //{
        //    var data = db.HCM_LeaveBalance.Where(x => x.IsActive == true).ToList();
        //    return data;
        //}


        #endregion------------------------------------------------------------------------------------

        public int SaveDBW(RajeshDemoModel demoModel)
        {
            foreach (var item in demoModel.dBW_BILLINGs)
            {
                var data = db.DBW_Mst_DPR.Where(x => x.Dpr_Id == item.Dpr_Id).FirstOrDefault();
                Demo_Table_Rajesh dm = new Demo_Table_Rajesh();
                dm.Length = item.Length;
                dm.Breadth = item.Breadth;
                dm.Height = item.Height;
                dm.Content_Area = item.Content_Area;
                dm.Quantity = item.Quantity;
                dm.Remark = item.Remark;
                dm.Service_Code = data.Service_Code;
                dm.Description_of_work = data.Description_of_work;
                dm.Application_Number = demoModel.ApplicationNo;
                dm.Unit = data.Unit;
                dm.Rate_per_Unit = data.Rate_per_Unit;
                dm.IsActive = true;
                dm.IsDelete = false;
                //dm.Application_Number = "Abcd1234";
                db.Demo_Table_Rajesh.Add(dm);
                db.SaveChanges();

            }
            return 1;

        }
        public List<Vw_DprEstimation> vw_DprEstimations()
        {
            var data = db.Vw_DprEstimation.Distinct().ToList();
            return data;
        }
       

        public List<Demo_Table_Rajesh> demo_Table_Rajeshes()
        {
            var data = db.Demo_Table_Rajesh.ToList();
            return data;
        }
    }
}
