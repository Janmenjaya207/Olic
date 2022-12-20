using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BEL
{
    public class FarmerRegistration
    {
        public DeepBorewell_Registration DB_Registration { get; set; }
       
       public List<DeepBorewell_PattaDetailsdetails> listdeeppatta { get; set; }

    }

    public class Paymentstatus
    {
        public List<Vw_DeepBorewellRegdDtl> Vw_DeepBorewellRegdDtl { get; set; }
        public BankUpdatePayment BankUpdatePayment { get; set; }
       
    }
    public class ShowRegistratondetails
    {
        public List<sp_DeepBorewell_PattaDetails_Result> sp_DeepBorewell_PattaDetails_Result { get; set; }
        public List<sp_deepBorewell_REgdeatils_Result> sp_deepBorewell_REgdeatils_Result { get; set; }
    }
    public class DeepBorewell_PattaDetailsdetails
    {
        public string Plot { get; set; }
        public string Khata { get; set; }
        public string Land { get; set; }
        public string Patta { get; set; }
    }
  public class ApprovalprocessEE
    {public EE_Verify EE_Verify { get; set; }
        public List<Applicationid> ListAssign { get; set; }
    }
    public class Applicationid
    {
        public int Applicationidd { get; set; }
     
    }
}
