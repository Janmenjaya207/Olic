using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
   public class Scrap_Details_Entry_MainModel
    {

        public List<Scrap_Details> Scrap_Details { get; set; }
        public List<sp_scrap_item_detail_category_wise_Result> sp_Scrap_Item_Detail_Category_Wise_Results { get; set; }
        public List<sp_scrap_detail_category_Result> sp_Scrap_Detail_Category_Results { get; set; }
        public int rowcount { get; set; }

        public string Remark { get; set; }
    }


    public class Scrap_Details
    {
        public int Scrap_Category_Id { get; set; }

        public int Scrap_Iteam_Id { get; set; }


        public string rate { get; set; }
        public string amount { get; set; }

        public string aw { get; set; }

        public string tw { get; set; }

        public DateTime dor { get; set; }
        public string no_of_scrap { get; set; }

        public string Remark { get; set; }


    }


}
