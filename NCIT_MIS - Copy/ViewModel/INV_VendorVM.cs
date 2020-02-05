using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class INV_VendorVM
    {

        public int VendorId { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string VendorName { get; set; }

        public string VendorCode { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string PanNo { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }
    }
}