using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BEL
{
    public class OlicMstDtl_SAP
    {
        public string circle { get; set; }
        public string circleDesc { get; set; }
        public string division { get; set; }
        public string divisionDesc { get; set; }
        public string subDivision { get; set; }
        public string subDivisionDesc { get; set; }
        public string section { get; set; }
        public string sectionDesc { get; set; }
    }

    public class CircleMaster_SAP
    {
        public string circle { get; set; }
        public string circleDesc { get; set; }
    }

    public class DivisionMaster_SAP
    {
        public string division { get; set; }
        public string circle { get; set; }
        public string divisionDesc { get; set; }
    }

    public class SubDivisionMaster_SAP
    {
        public string subDivision { get; set; }
        public string division { get; set; }
        public string subDivisionDesc { get; set; }
    }

    public class SectionMaster_SAP
    {
        public string section { get; set; }
        public string subDivision { get; set; }
        public string sectionDesc { get; set; }
    }
}
