using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class A_LocationVM
    {
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public string LocationCode { get; set; }

        public int DepartmentId { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int EnteredBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int LastUpdatedBy { get; set; }

        public string DepartmentName { get; set; }
    }
}