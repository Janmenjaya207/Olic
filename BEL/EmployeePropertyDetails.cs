using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{


    public class EmployeePropertyDetails
    {
        public Hcm_Property_Land DB_RegistrationLand { get; set; }
        public Hcm_Property_House DB_RegistrationHouse { get; set; }
        public Hcm_Property_ImmovableProperties DB_RegistrationImmovable { get; set; }
        public Hcm_Property_MovableProperty DB_RegistrationMovable { get; set; }
        public Hcm_Property_OtherMovable DB_RegistrationOtherMovable { get; set; }


        public List<Landss> ListOfLands { get; set; }
        public List<Houses> ListOfHouses { get; set; }
        public List<Immovableproperties> ListOfImmovableproperties { get; set; }

        public List<MovableProperties> ListOfMovableproperties { get; set; }
        public List<Othermovables> ListOfOthermovables { get; set; }

    }

    //public class Lands
    //{
    //    public int Id { get; set; }
    //    public string PreciseLocation { get; set; }
    //    public string ExtentOfInterest { get; set; }
    //    public Nullable<int> Value { get; set; }
    //    public string InWhoseName { get; set; }
    //    public string DateAndMannerOfAcquisitionOrDisposal { get; set; }


    //}
    public class Landss
    {
        public int Id { get; set; }
        public string PreciseLocation { get; set; }
        public string ExtentOfInterest { get; set; }
        public Nullable<int> Value { get; set; }
        public string InWhoseName { get; set; }
        public DateTime DateAndMannerOfAcquisitionOrDisposal { get; set; }
        public string Files { get; set; }
        public string EmpCode { get; set; }

    }
    public class Houses
    {
        public int Id { get; set; }
        public string PreciseLocation { get; set; }
        public string ExtentLOfInterest { get; set; }
        public Nullable<int> Value { get; set; }
        public string InWhoseName { get; set; }
        public Nullable<System.DateTime> DateAndManner { get; set; }
        public string Files { get; set; }
        public string EmpCode { get; set; }

    }

    public class Immovableproperties

    {
        public int Id { get; set; }
        public string BreifDescription { get; set; }
        public string ExtentOfInterest { get; set; }
        public Nullable<int> Value { get; set; }
        public string InWhoseName { get; set; }
        public DateTime DateAndMannerOfAcquisitionOrDiposal { get; set; }
        public string Remarks { get; set; }
        public string Files { get; set; }
        public int Employee_Id { get; set; }
        public string EmpCode { get; set; }

    }

    public class MovableProperties
    {
        public int Id { get; set; }
        public string DescriptionOfItems { get; set; }
        public Nullable<int> Value { get; set; }
        public string InWhoseName { get; set; }
        public Nullable<System.DateTime> DateAndManner { get; set; }
        public string Loans { get; set; }
        public string Files { get; set; }
        public string Remarks { get; set; }
        public string EmpCode { get; set; }

    }

    public class Othermovables
    {
        public int Id { get; set; }
        public string DescriptionOfItems { get; set; }
        public Nullable<int> Value { get; set; }
        public string InWhoseName { get; set; }
        public Nullable<System.DateTime> DateAndManner { get; set; }
        public string Files { get; set; }
        public string Remarks { get; set; }
        public string EmpCode { get; set; }

    }
}
