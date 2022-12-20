using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
  public  class RajeshDemoModel
    {
        public List<DBW_BILLING> dBW_BILLINGs { get; set; }

        public string ApplicationNo { get; set; }
        public List<Vw_DprEstimation> vw_DprEstimations { get; set; }

    }

    public class DBW_BILLING
    {
        public string Length { get; set; }

        public string Breadth { get; set; }

        public string Height { get; set; }

        public string Content_Area { get; set; }

        public string Quantity { get; set; }

        public int Dpr_Id { get; set; }

        public string Remark { get; set; }

    }
}
