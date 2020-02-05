using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class INV_UnitVM
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }

        public string UnitCode { get; set; }
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }
    }
}