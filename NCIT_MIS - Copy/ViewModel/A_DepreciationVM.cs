using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class A_DepreciationVM
    {

        public int DepreciationId { get; set; }

        public int DepartmentId { get; set; }

        public string DepreciationName { get; set; }

        public decimal? DepreciationRate { get; set; }

        public string Description { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string DepartmentName { get; set; }
    }
}