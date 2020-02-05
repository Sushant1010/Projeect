using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class SystemUserTypeVM
    {
        public int UserTypeId { get; set; }

        public int DepartmentId { get; set; }

        public string UserTypeName { get; set; }

        public string DepartmentName { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }
    }
}