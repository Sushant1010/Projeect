using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class DepartmentVM
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int PhoneNo { get; set; }

        public string Email { get; set; }

        public string DepartmentCode { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int EnteredBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int LastUpdatedBy { get; set; }
    }
}