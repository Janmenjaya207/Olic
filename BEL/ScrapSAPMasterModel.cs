using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
   public class ScrapSAPMasterModel
    {
        public string materialType { get; set; }

        public string materialTypeText { get; set; }

    }


    public class Scrap_Iteam_master_Model
    {
        public string matreial { get; set; }
        public string materialText { get; set; }

        public string materialType { get; set; }
    }


    public class Get_Sap_Code_fROM_Table
    {
        public string MaterialType { get; set; }
        public string Matreial_Code { get; set; }

        public string User_Type { get; set; }

        public string Unit { get; set; }

        public string noOfQuantity { get; set; }

        public string rate { get; set; }

        public string amount { get; set; }
        public string circle { get; set; }

        public string section { get; set; }

        public string subdivision { get; set; }

        public string approxWeight { get; set; }

        public string totalWeight { get; set; }

    }

    
}
