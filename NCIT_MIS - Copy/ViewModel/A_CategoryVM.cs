using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class A_CategoryVM
    {
        public int CategoryId { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string CategoryName { get; set; }

        public string CategoryCode { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }
    }
}