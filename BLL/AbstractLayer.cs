using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BEL;

namespace BLL
{
    public interface AbstractLayer
    {
        ManageUser CheckLogin(string username, string password);
        ManageUser Mst_Ho(int id);
        int savemanageuser(ManageUsermodel nh);
        bool DeleteMangeuser(int id);
        List<SelectListItem> Getsection();
        List<SelectListItem> GetDivision();
        int Deleteapplyleave(int id);
        List<SelectListItem> GetSubDivision();
        List<SelectListItem> GetCircle();
        List<SelectListItem> GetUserRole();
        List<OLIC_Test> oLIC_Tests();

        string vinno();
        int SaveDeepBorewellApplication(FarmerRegistration FarmerRegd);

        List<SelectListItem> District();
        List<SelectListItem> Block();
        List<SelectListItem> GramPanchayat();

        List<Vw_DeepBorewellRegdDtl> DbAppDetails();
        List<sp_DeepBorewell_PattaDetails_Result> sp_DeepBorewell_PattaDetails_Result(int id, string url);
        List<sp_deepBorewell_REgdeatils_Result> sp_deepBorewell_REgdeatils_Result(int id);
        int SavePaymentStatus(Paymentstatus Paymentstatus);
        int Saveverify_application(ApprovalprocessEE bg);
        List<SelectListItem> EEBlock();
        List<SelectListItem> EEGramPanchayat();


        int SaveDBWMstDPR(MasterModel DprMasterModel);
        List<DBW_Mst_DPR> dBW_Mst_DPRs();
        //bool Delete_DBW_DPR_Master(int id);

        //int SaveHcmEmp(MasterModel hcm_EmployeeModel);


        int SaveOlicMaster(List<OlicMstDtl_SAP> olicMstDtl_SAP);

        int SaveCircleMaster(List<CircleMaster_SAP>circleMaster_SAPs);
        List<Mst_Circle> mst_Circles();

        int SaveDivisionMaster(IList<DivisionMaster_SAP>divisionMaster_SAPs);
        List<Mst_Division> mst_Divisions();

        int SaveSubDivisionMaster(IList<SubDivisionMaster_SAP> subDivisionMaster_SAPs);
        List<Mst_SubDivision> mst_SubDivisions();

        int SaveSectionMaster(IList<SectionMaster_SAP>sectionMaster_SAPs);
        List<Mst_Section> mst_Sections();

        #region--------------------------------------Scrap---------------------------------------------------------
        int Savescrap_Category(List<ScrapSAPMasterModel> olicMstDtl_SAP);

        int Scrap_Item_Mst(List<Scrap_Iteam_master_Model> Scrap_Iteam_master_Model);

        //int Scrap_Details_Save(List<Scrap_Details> sdetail);
        int Scrap_Details_Save(Scrap_Details_Entry_MainModel sdetail);

        string Scrap_Code();
        #endregion---------------------------------------------------------------------------------------------------

        #region-----------------------------------------HCM-----------------------------------------------------
        List<HCM_EmployeeDtl> hcm_EmployeeDtls();
        List<Hcm_Property_Land> hcm_LandDtls();
        List<Hcm_Property_House> hcm_HouseDtls();
        List<Hcm_Property_Land> hcm_LandDtls(string uid);
        List<Hcm_Property_House> hcm_HouseDtls(string uid);
        List<vwEmployeesByLeaveType> hcm_LeaveDetails(int id);
        List<vwEmployeesByLeaveType> LeaveDetailss(int id);
       
        List<vwEmployeesByLeaveType> LeaveDetailsssssss();
        List<Hcm_Property_ImmovableProperties> hcm_ImmovablepropertyDtls();
        List<Hcm_Property_MovableProperty> hcm_MovablepropertyDtls();
        List<Hcm_Property_OtherMovable> hcm_OMovablepropertyDtls();
        List<Hcm_Property_ImmovableProperties> hcm_ImmovablepropertyDtls(string uid);
        List<Hcm_Property_MovableProperty> hcm_MovablepropertyDtls(string uid);
        List<Hcm_Property_OtherMovable> hcm_OMovablepropertyDtls(string uid);
        List<Hcm_Property> hcm_PropertyDtls();
        List<HCM_LeaveBalance> hcm_LeaveMasterDtls(string uname);
        int SaveHcmEmp(EmployeePropertyDetails employeePropertyDetails);
        int EditHcmEmp(EmployeePropertyDetails employeePropertyDetails);
        List<SelectListItem> Leavetypelist();
        int saveapplyleave(ApplyLeaveModel leave);
        int Savehcmemployeedetails(List<HCM_EmployeeDetails> circleMaster_SAPs);
        bool Resolve(string regid, int id, string remark);
        bool Reject(string regid, int id, string remark);

        List<vwEmployeesByLeaveType> hcm_LeaveDetails(string id);
        List<vwEmployeesByLeaveType> LeaveDetailss(string id);
        //int LeaveDetailsss(DeleteLeaveModel nhh);
        //int hcm_LeaveDetailss();
        //int Deleteapplyleave(int id);
        

        #endregion---------------------------------------------------------------------------------------------
        List<Vw_DprEstimation> vw_DprEstimations();
        int SaveDBW(RajeshDemoModel demoModel);

        List<Demo_Table_Rajesh> demo_Table_Rajeshes();
    }
}
